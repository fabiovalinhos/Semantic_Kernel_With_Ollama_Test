using Microsoft.SemanticKernel;

namespace Semantic_Kernel_With_Ollama_Test.Plug
{
    public class HoraAtualPlugin
    {
        [KernelFunction]
        public string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
    }
}