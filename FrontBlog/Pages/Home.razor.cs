using FrontBlog.Helpers;

namespace FrontBlog.Pages
{
    public partial class Home
    {
        private async Task DemoSucces()
        {
            await JsRuntime.ToastrSuccess("Tarea completada!");
        }

        private async Task DemoError()
        {
            await JsRuntime.ToastrError("Error, Tarea incompleta!");
        }
    }
}