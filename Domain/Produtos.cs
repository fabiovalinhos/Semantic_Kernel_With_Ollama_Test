using System.ComponentModel.DataAnnotations;

namespace Semantic_Kernel_With_Ollama_Test.Domain
{
    public class Produtos
    {
        public Produtos()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public int IdProduto { get; set; }

        public string Nome { get; set; }

        public int Quantidade { get; set; }

        public string DescricaoBreve { get; set; }
    }
}