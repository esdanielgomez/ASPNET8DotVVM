using DotVVM.AutoUI;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;
using Microsoft.Extensions.DependencyInjection;
namespace App.PL;

public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
{
    public void Configure(DotvvmConfiguration config, string applicationPath)
    {
        ConfigureRoutes(config, applicationPath);
        ConfigureControls(config, applicationPath);
        ConfigureResources(config, applicationPath);
    }

    private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
    {
        config.RouteTable.Add("Default", "", "Views/Default.dothtml");
        config.RouteTable.Add("CRUD_Create", "create", "Views/CRUD/Create.dothtml");
        config.RouteTable.Add("CRUD_Detail", "detail/{Id}", "Views/CRUD/Detail.dothtml");
        config.RouteTable.Add("CRUD_Edit", "edit/{Id}", "Views/CRUD/Edit.dothtml");
        config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
    }

    private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
    {
        // register code-only controls and markup controls
    }

    private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
    {
        config.Resources.Register("Styles", new StylesheetResource()
        {
            Location = new UrlResourceLocation("~/styles.css")
        });
    }

    public void ConfigureServices(IDotvvmServiceCollection options)
    {
        options.AddDefaultTempStorages("temp");
        options.AddHotReload();
        options.AddAutoUI(uioptions =>
        {
            uioptions.AutoDiscoverFormEditorProviders(typeof(DotvvmStartup).Assembly);
            uioptions.AutoDiscoverGridColumnProviders(typeof(DotvvmStartup).Assembly);
        });
    }
}