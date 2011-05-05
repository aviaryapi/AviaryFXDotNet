using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Configuration;

using CartonApi;
using CartonApi.Rest;
using CartonApi.ResponseType;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    //the height / width of the thummbnails to be generated
    protected int thumbWidth = 125;
    protected int thumbHeight = 125;


    protected void Page_Load(object sender, EventArgs e)
    {
        btnUpload.Click += new EventHandler(btnUpload_Click);
        ddlFilters.SelectedIndexChanged += new EventHandler(ddlFilters_SelectedIndexChanged);
        rptThumbs.ItemCommand += new RepeaterCommandEventHandler(rptThumbs_ItemCommand);
    }

    //handle the postback from the repeater
    void rptThumbs_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (ddlFilters.SelectedValue != "0")
        {
            string viewStateKey = ((Label)e.Item.FindControl("lblViewStateKey")).Text;
            Render renderParams = ViewState[viewStateKey] as Render;
            
            CartonApi.AviaryFX.OutputParameters outputParams = new CartonApi.AviaryFX.OutputParameters(CartonApi.AviaryFX.OutputParameters.FileType.jpg, "0xFFFFFFFF", 80, 1.0);
            AviaryFXRender render = this.Api.AviaryFX.Render(lblAviaryFXFileName.Text, Convert.ToInt32(ddlFilters.SelectedValue), outputParams, renderParams.Parameters, 500, 400);


            imageArea.Src = render.Url;
        }
    }

    //Handle the user selecting a filter
    void ddlFilters_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFilters.SelectedValue != "0")
        {
            CartonApi.AviaryFX.OutputParameters outputParams = new CartonApi.AviaryFX.OutputParameters(CartonApi.AviaryFX.OutputParameters.FileType.jpg, "0xFFFFFFFF", 80, 1.0);
            CartonApi.AviaryFXRender render = this.Api.AviaryFX.RenderThumbGrid(lblAviaryFXFileName.Text, Convert.ToInt32(ddlFilters.SelectedValue), outputParams, null, 3, 3, thumbWidth, thumbHeight);

            lblAviaryFXThumbsURL.Text = render.Url;

            rptThumbs.ItemDataBound += new RepeaterItemEventHandler(rptThumbs_ItemDataBound);
            rptThumbs.DataSource = render.Renders;
            rptThumbs.DataBind();
        }
    }

    //Store the render parameters with the thumbnail, so if the user selectes this image
    //we can generate a full size final image
    int thumbPosition = 0;
    void rptThumbs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Render render = e.Item.DataItem as Render;
        
        string viewStateKey = "thumb" + thumbPosition;

        ViewState.Add(viewStateKey, render);
        Label lblThumbID = e.Item.FindControl("lblViewStateKey") as Label;
        lblThumbID.Text = viewStateKey;

        thumbPosition++;
    }

    //Upload file to the AviaryFX server
    void btnUpload_Click(object sender, EventArgs e)
    {
        AviaryFXFile aviaryFXFile = null;
        if (fileUpload.HasFile)
        {
            aviaryFXFile = UploadToAviaryFX(fileUpload.FileName, fileUpload.FileBytes);

            imageArea.Src = aviaryFXFile.Path;
            lblAviaryFXFileName.Text = aviaryFXFile.Path;

            uploadForm.Visible = false;

            List<CartonApi.FilterInfo> filters = GetFilters();

            effectOptions.Visible = true;
            ddlFilters.DataTextField = "FilterName";
            ddlFilters.DataValueField = "FilterID";
            ddlFilters.DataSource = filters;
            ddlFilters.DataBind();

            ddlFilters.Items.Insert(0, new ListItem("Select an effect", "0"));
        }
        else
        {
            
        }
    }

    //Get the list of filters
    protected List<CartonApi.FilterInfo> GetFilters()
    {
        List<CartonApi.FilterInfo> filters = this.Api.Filter.GetFilters();
        return filters;
    }

    //Upload a file to AviaryFX server
    protected AviaryFXFile UploadToAviaryFX(string fileName_, byte[] fileStream_)
    {
        AviaryFXFile aviaryFXFile = this.Api.AviaryFX.Upload(fileName_, fileStream_);
        return aviaryFXFile;
    }



    private Api _Api = null;
    public Api Api
    {
        get
        {
            if (_Api == null)
            {
                _Api = new Api(new ApiClient(ConfigurationManager.AppSettings["APIKey"], ConfigurationManager.AppSettings["APISecret"]));
                _Api.AviaryFXVersion = ConfigurationManager.AppSettings["APIVersion"];
            }
            return _Api;
        }
    }
}
