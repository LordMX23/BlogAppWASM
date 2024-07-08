using FrontBlog.Helpers;
using FrontBlog.Modelos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace FrontBlog.Pages.Posts
{
    public partial class CompPosts
    {
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();

        private bool estaProcesando = false;
        private int? BorrarIdPost { get; set; } = null;
        protected override async Task OnInitializedAsync()
        {
            Posts = await postsServicio.GetPosts();
        }

        public async Task ManejadorOnBorrar(int PostId)
        {
            BorrarIdPost = PostId;
            await JsRuntime.InvokeVoidAsync("MostrarModalConfirmacionBorrado");
        }

        public async Task Click_Confirmacion_Borrado(bool confirmado)
        {
            estaProcesando = true;
            if(confirmado && BorrarIdPost != null)
            {
                //Post post = await postsServicio.GetPost(BorrarIdPost.Value);
                await postsServicio.EliminarPost(BorrarIdPost.Value);
                await JsRuntime.ToastrSuccess("Post borrado correctamente");
                Posts = await postsServicio.GetPosts();
            }

            await JsRuntime.InvokeVoidAsync("OcultarModalConfirmacionBorrado");
            estaProcesando=false;
        }
    }
}