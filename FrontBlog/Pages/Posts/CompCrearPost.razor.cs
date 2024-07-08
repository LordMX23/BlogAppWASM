using FrontBlog.Helpers;
using FrontBlog.Modelos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace FrontBlog.Pages.Posts
{
    public partial class CompCrearPost
    {
        private Post CrearPost { get; set; } = new Post();

        [Parameter]
        public string imagenPost { get; set; } = "";
        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        private async Task MenjadorOnCrearPost()
        {
            this.CrearPost.RutaImagen = imagenPost;
            var CrearPost = await postsServicio.CrearPost(this.CrearPost);
            await JsRuntime.ToastrSuccess("Post creado correctamente");
            navigationManager.NavigateTo("posts");
        }

        private async Task ManejadorOnSubidaArchivo(InputFileChangeEventArgs e)
        {
            var imageFile = e.File;
            if (imageFile != null)
            {
                var resizedFile = await imageFile.RequestImageFileAsync("image/png", 1000, 700);
                using (var ms = resizedFile.OpenReadStream(resizedFile.Size))
                {
                    var content = new MultipartFormDataContent();
                    content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    content.Add(new StreamContent(ms, Convert.ToInt32(resizedFile.Size)), "image", imageFile.Name);
                    imagenPost = await postsServicio.SubirImagenPost(content);
                    await OnChange.InvokeAsync(imagenPost);
                }
            }
        }
    }
}