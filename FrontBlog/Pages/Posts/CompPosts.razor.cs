using FrontBlog.Modelos;

namespace FrontBlog.Pages.Posts
{
    public partial class CompPosts
    {
        public IEnumerable<Post> Posts { get; set; } = new List<Post>();

        private bool estaProcesando = false;
        protected override async Task OnInitializedAsync()
        {
            Posts = await postsServicio.GetPosts();
        }

        public void ManejadorOnBorrar(int PostId) { }
    }
}