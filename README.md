# Olive.GlobalSearch

A distributed enterprise search solution.

## Installing the UI component

1. In your UI project (e.g. [Access Hub](https://geeksltd.github.io/Olive/#/Microservices/Overview?id=distributed-ui-via-access-hub)) add the [Olive.GlobalSearch.UI](https://www.nuget.org/packages/Olive.GlobalSearch.UI/) nuget package.

2. Add an auto-complete control where in the template you want to show the textbox for search:
```html
<input type="text" asp-for="Keywords" placeholder="Search..." class="form-control auto-complete" autocomplete="off"
       autocomplete-source="@(Url.ActionWithQuery("GlobalSearch/AutoComplete"))" />
```

3. Add the following controller action to your application in a new controller file.
```c#
[HttpPost, Route("GlobalSearch/AutoComplete")]
public Task<ActionResult> ServiceSource(Olive.GlobalSearchViewModel info) => Olive.GlobalSearch.AutoComplete(info);
```
   
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
