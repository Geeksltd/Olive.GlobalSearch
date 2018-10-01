# Olive.GlobalSearch

> Note: This project is depricated and its's in archive mode. We added GlobalSearch to Olive.MVC as a plugin.

It's a distributed enterprise search solution.

Enterprise applications that are based on Microservices, are split into several small applications each with its own database, business rules and content. However, there is a need for the end-user to have a central place to run a search to find appropriate business data, regardless of where it's actually hosted.

**Olive.GlobalSearch** provides a solution to make that happen in the easiest way possible.

## How does it work?

It consists of a UI component, where the user will type in some keywords to search. The search query will then be passed on to various individual micro-services via HTTP-based Apis. They will each then provide their own *"participation"*, i.e. search results for the same keywords. The UI component will then show all results across all services in a single auto-complete list.

Each result item will have a Title (mandatory), Description, DestinationUrl (mandatory) and IconUrl. They can also provide a css style for the item so that they can be styled differently. For example depending on the type of the content item, it can be shown in a different colour in the auto-complete.

### Installing the UI component

1. Add an auto-complete control where in the template you want to show the textbox for search:

```html
<input type="text" name="searcher" placeholder="Search..."
              class="form-control global-search"
              globalsearch-source="@(Url.ActionWithQuery("GlobalSearch/AutoComplete"))" />
```

2. Add the following controller action to your application in a new controller file.

```c#
[HttpGet, Route("GlobalSearch/AutoComplete")]
        public async Task<ActionResult> GetServiceSource() =>
        Json(Config.SettingsUnder("Olive.GlobalSearch:Sources").Select(x => x.Value).ToArray());
```

3. In `appSettings.json` add:

```json
   "Olive.GlobalSearch": {
       "Sources": [
         { "My Site 1": "http://mysite1.com/api/global-search" },
         { "My Site 2": "http://mysite2.com/global-search.axd" },
         ...
       ]
   }
```

![image](https://user-images.githubusercontent.com/22152065/39919148-fe2dfe46-5527-11e8-8f10-98336c885de5.png)

## Installing Search providers

In each microservice that contributes to search results (perhaps including Access Hub itself) add the following:

### Step 1
Add the [Olive.GlobalSearch.Source](https://www.nuget.org/packages/Olive.GlobalSearch.Source/) nuget package.

### Step 2
Add the following class to the Website project:

```c#
public class GlobalSearchSource : Olive.GlobalSearch.SearchSource
{
     public override async Task Process(ClaimsPrincipal user)
     {
         // TODO: Process the keywords and add result items.
                     
         foreach (var something in await SomeThings...())
         {
             if (MatchesKeywords(something))
                 Add(new SearchResult { Url = "...", Title = "...", Description = "...", IconUrl = "..." });
         }        
         
         if (user.IsInRole("Administrator"))
         {
             // Role specific results ...
         }
     }
}
```

### Step 3 (.NET Core Apps)


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


### Step 3 (.Net 4.6+ Apps)
For legacy ASP.NET applications, add the following code to Web.config:

```xml
<configuration>
   <system.webServer>
      <handlers>
         <add name="Olive Global Search" verb="GET" path="global-search.axd" type="GlobalSearchSource" />
      </handlers>
   <system.webServer>
</configuration>
```
