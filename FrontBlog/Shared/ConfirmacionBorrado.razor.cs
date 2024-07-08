using Microsoft.AspNetCore.Components;

namespace FrontBlog.Shared
{
    public partial class ConfirmacionBorrado
    {
        public bool procesoIniciado { get; set; } = false;
        [Parameter]
        public EventCallback<bool> CambioConfirmacion { get; set; }
        [Parameter]
        public bool ProcesandoComponentePadre { get; set; }

        protected override void OnParametersSet()
        {
            procesoIniciado = ProcesandoComponentePadre;
        }

        protected async Task ConfirmacionOnCambia(bool valor)
        {
            if (valor)
            {
                procesoIniciado = true;
            }
            await CambioConfirmacion.InvokeAsync(valor);
        }
    }
}