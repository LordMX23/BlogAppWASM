using FrontBlog.Helpers;
using FrontBlog.Modelos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace FrontBlog.Pages.Posts
{
    public partial class CompEditarPost
    {
        private Post EditaPost { get; set; } = new Post();
        [Parameter]
        public int? Id { get; set; }
        [Parameter]
        public string imagenPost { get; set; } = "";
        [Parameter]
        public EventCallback<string> OnChange { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EditaPost = await postsServicio.GetPost(Id.Value);
        }

        private async Task MenjadorOnEditarPost()
        {
            this.EditaPost.RutaImagen = imagenPost;
            var editarPost = await postsServicio.ActualizarPost(Id.Value, this.EditaPost);
            await JsRuntime.ToastrSuccess("Post actualizado correctamente");
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