using FrontBlog.Modelos;

namespace FrontBlog.Servicios.IServicio
{
    public interface IPostServicio
    {
        public Task<IEnumerable<Post>> GetPosts();
        public Task<Post> GetPost(int PostId);
        public Task<Post> CrearPost(Post post);
        public Task<Post> ActualizarPost(int PostId, Post post);
        public Task<bool> EliminarPost(int PostId);
        public Task<string> SubirImagenPost(MultipartFormDataContent content);
    }
}
