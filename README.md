# Aviary Effects API ASP.Net Library

## Introduction

A library for the Aviary Effects API written in ASP.Net.

## Instantiate an API object

The first step to accessing any of the API features is to instantiate a CartonAPI.Rest.Api object. This is done as follows:

<pre><code>CartonApi.Rest.Api api = new CartonApi.Rest.Api( 
new CartonApi.Rest.ApiClient( {YOUR API KEY}, {YOUR API SECRET} ));
</code></pre>

Once an Api object has been instantiated, you can access the Api Rest libraries by calling: api.AviaryFx.Upload(...), etc

## Upload an image to the AviaryFX service

Before applying any filters, you'll need to upload your file to the AviaryFX servuce using the AviaryFX.Upload() method. The Upload() method returns an AviaryFXFile object that should be used for future interactions with the AviaryFX library.

## Get a list of Filters

You can retrieve a list of filters that can be applied to your photo. This is done by calling the Filter.GetFilters() method which returns a List of Filter objects. You can skip this if you already know which effect you'd like to apply. We recommend calling this every once in a while to see if new effects have been added.

## Select Render Parameters and Render Image

Next you'll need set up your RenderParameters. These parameters tell API exactly what effect to render. You can tweak these settings to achieve subtly differences and tweaks in your image. You can get these parameters and their values by experimenting in our [filter sandbox](http://developers.aviary.com/filtersandbox), looking at the [parameter descriptions](http://developers.aviary.com/filter-list) or by generating a random thumbnail grid as described below. Once a RenderParameter list has been selected, it is passed to AviaryFX.Render() along with the FilterID, and AviaryFxFile.Path. Another AviaryFXRender object is returned with a url to the final image.

## Render thumbnails

If you'd like to see multiple versions of you image as you'd see with different parameter values, you can call AviaryFX.RenderThumbGrid() to render a thumbnail grid. It returns an AviaryFXRender object which contains a url to the thumbnail grid, and a List of Render objects which describe each element of the grid. Each of the render objects contains a RenderParameter list that can be passed to AviaryFX.Render() to reproduce that particular effect on a full image.

## Save filter settings

You can save a RenderParameter list that you like and store it for future use. This way you can go straight to rendering the full image with that RenderParameter list for future uses.

## Quickstart Code

The following code sample will populate a working Api object.

<pre><code>CartonApi.Rest.Api api = new CartonApi.Rest.Api(new CartonApi.Rest.ApiClient(api_key, api_secret));</code></pre>

## Examples

### Get a list of filters

<pre><code>Api api = new CartonApi.Api(new CartonApi.ApiClient(api_key, api_secret)); 
List filters = api.Filter.GetFilters();
</code></pre>

### Upload an image to the AviaryFX Service

<pre><code>byte[] fileData = imageFile.FileBytes;
string original_filename = imageFile.FileName; 

CartonApi.AviaryFXFile file = api.AviaryFX.Upload(original_filename, fileData);
</code></pre>

### Render full image

<pre><code>byte[] fileData = imageFile.FileBytes;
string original_filename = imageFile.FileName; 

CartonApi.AviaryFXFile file = api.AviaryFX.Upload(original_filename, fileData);
CartonApi.AviaryFX.OutputParameters outputParams = new CartonApi.AviaryFX.OutputParameters(
CartonApi.AviaryFX.OutputParameters.FileType.jpg, "0xFFFFFFFF", 80, 1.0); 

// render a thumbnail grid.
CartonApi.AviaryFXRender render = api.AviaryFX.RenderThumbGrid(file.Path, 
filter.FilterID, outputParams, null, 2, 2); 

// choose a set of render parameters and do a full render.
render = api.AviaryFX.Render(file.Path, filter.FilterID, outputParams, render.Renders[0].Parameters)
</code><pre>

###  Render thumbnail grid

<pre><code>byte[] fileData = imageFile.FileBytes;
string original_filename = imageFile.FileName; 

CartonApi.AviaryFXFile file = api.AviaryFX.Upload(original_filename, fileData); 

CartonApi.AviaryFX.OutputParameters outputParams = new CartonApi.AviaryFX.OutputParameters(
CartonApi.AviaryFX.OutputParameters.FileType.jpg, "0xFFFFFFFF", 80, 1.0); 

CartonApi.AviaryFXRender render = api.AviaryFX.RenderThumbGrid(file.Path,
filter.FilterID, outputParams, null, 2, 2);
</code></pre>

## Methods

Check out the official [Aviary Effects API documentation](http://developers.aviary.com/effects-api) for more details about the Aviary Effects API and class methods.

## Feedback and questions

Found a bug or missing a feature? Don't hesitate to create a new issue here on GitHub, post to the [Google Group](http://groups.google.com/group/aviaryapi) or email us.
