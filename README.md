# Olive.GlobalSearch

It's a distributed enterprise search solution.

Enterprise applications that are based on Microservices, are split into several small applications each with its own database, business rules and content. However, there is a need for the end-user to have a central place to run a search to find appropriate business data, regardless of where it's actually hosted.

**Olive.GlobalSearch** provides a solution to make that happen in the easiest way possible.

## How does it work?

It consists of a UI component, where the user will type in some keywords to search. The search query will then be passed on to various individual micro-services via HTTP-based Apis. They will each then provide their own *"participation"*, i.e. search results for the same keywords. The UI component will then show all results across all services in a single auto-complete list. 

Each result item will have a Title (mandatory), Description, DestinationUrl (mandatory) and IconUrl. They can also provide a **Liquid template** for the item so that they can be styled differently. For example dependeing on the type of the content item, it can be shown in a different colour in the auto-complete.

### Installing the UI component

1. In your UI project (e.g. [Access Hub](https://geeksltd.github.io/Olive/#/Microservices/Overview?id=distributed-ui-via-access-hub)) add the [Olive.GlobalSearch.UI](https://www.nuget.org/packages/Olive.GlobalSearch.UI/) nuget package.

2. Add an auto-complete control where in the template you want to show the textbox for search:
```html
<input type="text" asp-for="Keywords" placeholder="Search..."
       class="form-control auto-complete" autocomplete="off"
       autocomplete-source="@(Url.ActionWithQuery("GlobalSearch/AutoComplete"))" />
```

> Don't forget to add a `Keywords` property into your ViewModel

> The `Search.GetResults(model.Keywords)` and other methods will have a fixed implementation that comes with the DLL. It gets the sources from the config file and invokes their APIs in parallel to get the results back. `AutoComplete()` will then combine them and return the results back to the client as a Json result, so the auto-complete provider can render them.

3. Add the following controller action to your application in a new controller file.

```c#
[HttpPost, Route("GlobalSearch/AutoComplete")]
public async Task<ActionResult> ServiceSource(ViewModel.MyViewModel model)
    => Json((await Search.GetResults(model.Keywords)).AutoComplete());
```

4. In `appSettings.json` add:

```json
   "Olive.GlobalSearch": {
       "Sources": [
         { "Site 1": "http://...." },
         { "Site 2": "http://...." },
         ...
       ]
   }
```

#### Server side rendering

By default, `(await Search.GetResults(model.Keywords)).AutoComplete()` method returns a json object which you need to render the UI in the front end. But what if you want to have a server-side rendering? Global search lets you to render the results in the server side.
`(await Search.GetResults(model.Keywords)).RenderHtml()` will put the results into the HTML then it returns rendered HTML to the client.

![image](https://user-images.githubusercontent.com/22152065/39919148-fe2dfe46-5527-11e8-8f10-98336c885de5.png)

##### Using custom styles and html template

You might be asking that how developers can use their own HTML template with some custom styles or maybe some java script and logic in the server side rendering. We used **Liquid template engine** which lets you to create your own template. If you're not familiar with the liquid and the syntax of it take a look at [Liquid documentation](http://shopify.github.io/liquid/basics/introduction/) and [the default template for Global Search](https://github.com/Geeksltd/Olive.GlobalSearch/blob/master/Olive.GlobalSearch/Olive.GlobalSearch.UI/Template.liquid).

> The resources that you can use in your template are : **url**, **iconurl**, **title** and **description**.

With `(await Search.GetResults(model.Keywords)).RenderHtml("my template full string")` you can render the view with your own template.

#### Client side orchestration

(Soon) ...
   
### Installing Search providers

In each microservice that contributes to search results (perhaps including Access Hub itself) add the following:

1. Add the [Olive.GlobalSearch.Source](https://www.nuget.org/packages/Olive.GlobalSearch.Source/) nuget package.

2. In StartUp.cs, add: 
```c#
public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    base.Configure(app, env);
    ...
    app.UseGlobalSearch<GlobalSearchSource>();
    ...
}
```

3. Add the following class:
```c#
public class GlobalSearchSource : Olive.GlobalSearch.SearchSource
{
     public override void Process(ClaimsPrincipal user, string[] keywords)
     {
         // TODO: Process the keywords and add result items.
         
         if (user.IsInRole("Administrator"))
             Return("Some url", "Some title", "Some description", icon: "Some url");
             
         if (...)
             Return("Some other url", "Some title", "Some description", icon: "Some url");
     }
}
```


