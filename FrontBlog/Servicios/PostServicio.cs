using FrontBlog.Helpers;
using FrontBlog.Modelos;
using FrontBlog.Servicios.IServicio;
using Newtonsoft.Json;
using System.Text;

namespace FrontBlog.Servicios
{
    public class PostServicio : IPostServicio
    {
        private readonly HttpClient _cliente;

        public PostServicio(HttpClient cliente)
        {
            _cliente = cliente;
        }
        public async Task<Post> ActualizarPost(int PostId, Post post)
        {
            var content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PatchAsync($"{Inicializar.UrlBaseApi}api/posts/{PostId}", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTempo = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTempo);
                return result;
            }
            else
            {
                var contentTempo = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(contentTempo);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<Post> CrearPost(Post post)
        {
            var content = JsonConvert.SerializeObject(post);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/posts", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var contentTempo = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Post>(contentTempo);
                return result;
            }
            else
            {
                var contentTempo = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(contentTempo);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<bool> EliminarPost(int PostId)
        {
            var response = await _cliente.DeleteAsync($"{Inicializar.UrlBaseApi}api/posts/{PostId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<Post> GetPost(int PostId)
        {
            var response = await _cliente.GetAsync($"{Inicializar.UrlBaseApi}api/posts/{PostId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var posts = JsonConvert.DeserializeObject<Post>(content);
                return posts;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var errorModel = JsonConvert.DeserializeObject<ModeloError>(content);
                throw new Exception(errorModel.ErrorMessage);
            }

        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var response = await _cliente.GetAsync($"{Inicializar.UrlBaseApi}api/posts");
            var content = await response.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<IEnumerable<Post>>(content);
            return posts;
        }

        public async Task<string> SubirImagenPost(MultipartFormDataContent content)
        {
            var postResult = await _cliente.PostAsync($"{Inicializar.UrlBaseApi}api/upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imagenPost = Path.Combine($"{Inicializar.UrlBaseApi}", postContent);
                return imagenPost;
            }
        }
    }
}
