using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace Semantic_Kernel_With_Ollama_Test.Plug
{
    public class HoraAtualPlugin
    {
        [KernelFunction]
        [Description("Responde a perguntas relacionadas à hora atual ou ao horário no momento.")]
        public string HoraAtual()
        {
            var agora = DateTime.Now;
            return $"Agora são {agora:HH:mm} do dia {agora:dd/MM/yyyy}.";
        }
    }
}