# Olive.GlobalSearch

It's a distributed enterprise search solution.

Enterprise applications that are based on Microservices, are split into several small applications each with its own database, business rules and content. However, there is a need for the end-user to have a central place to run a search to find appropriate business data, regardless of where it's actually hosted.

**Olive.GlobalSearch** provides a solution to make that happen in the easiest way possible.

## How does it work?
It consists of a UI component, where the user will type in some keywords to search. The search query will then be passed on to various individual micro-services via HTTP-based Apis. They will each then provide their own *"participation"*, i.e. search results for the same keywords. The UI component will then show all results across all services in a single auto-complete list. 

Each result item will have a Title (mandatory), Description, DestinationUrl (mandatory) and IconUrl. They can also provide a CssClass for the item so that they can be styled differently. For example dependeing on the type of the content item, it can be shown in a different colour in the auto-complete.

## Installing the UI component

1. In your UI project (e.g. [Access Hub](https://geeksltd.github.io/Olive/#/Microservices/Overview?id=distributed-ui-via-access-hub)) add the [Olive.GlobalSearch.UI](https://www.nuget.org/packages/Olive.GlobalSearch.UI/) nuget package.

2. Add an auto-complete control where in the template you want to show the textbox for search:
```html
<input type="text" asp-for="Keywords" placeholder="Search..."
       class="form-control auto-complete" autocomplete="off"
       autocomplete-source="@(Url.ActionWithQuery("GlobalSearch/AutoComplete"))" />
```

3. Add the following controller action to your application in a new controller file.
```c#
[HttpPost, Route("GlobalSearch/AutoComplete")]
public Task<ActionResult> ServiceSource(string keywords) => Olive.GlobalSearch.AutoComplete(keywords);
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

> The `GlobalSearch.AutoComplete(string keywords)` method will have a fixed implementation that comes with the DLL. It gets the sources from the config file and invokes their APIs in parallel to get the results back. It will then combine them and return the results back to the client as a Json result, so the auto-complete provider can render them.
   
## Installing Search providers
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
     public override void Process(IUser user, string[] keywords)
     {
         // TODO: Process the keywords and add result items.
         
         if (user.IsInRole("Administrator"))
             Return("Some url", "Some title", "Some description", icon: "Some url");
             
         if (...)
             Return("Some other url", "Some title", "Some description", icon: "Some url");
     }
}
```
