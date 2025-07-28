using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Semantic_Kernel_With_Ollama_Test.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchemaWithCorrectedLengths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdProduto = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    DescricaoBreve = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.UniqueConstraint("AK_Produtos_IdProduto", x => x.IdProduto);
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "DescricaoBreve", "IdProduto", "Nome", "Quantidade" },
                values: new object[,]
                {
                    { new Guid("a4b3c2d1-0e7f-4a6b-9c8d-7f1e4a3b2c8d"), "Armazenamento ultrarrápido para sistema e jogos.", 8, "SSD NVMe 1TB", 35 },
                    { new Guid("a8d3e4c2-3b1f-4f9e-8a7d-1b9c0f8e5d2a"), "Mouse com design confortável para longas horas de uso.", 2, "Mouse Sem Fio Ergonômico", 50 },
                    { new Guid("b3c9a1e8-5d2a-4f8c-9b7e-2a8d0f6c4b1e"), "Teclado com switches mecânicos e iluminação RGB.", 3, "Teclado Mecânico RGB", 30 },
                    { new Guid("b9c8d7e6-1f8a-4b9c-a7d6-8a2f5b4c3d9e"), "GPU de última geração para gráficos incríveis.", 9, "Placa de Vídeo RTX 4070", 10 },
                    { new Guid("c1d8e2f7-6a3b-4e9d-8c5f-3b7a0e9d6c4a"), "Monitor curvo para uma experiência imersiva.", 4, "Monitor Ultrawide 34\"", 20 },
                    { new Guid("c7d6e5f4-2a9b-4c8d-b6e5-9b3a6c4d2e8f"), "Gabinete com bom fluxo de ar e espaço interno.", 10, "Gabinete Mid-Tower", 22 },
                    { new Guid("d5e4f3a2-3b8c-4d7e-a5f4-0c4b7d3e1f9a"), "Fonte com certificação 80 Plus Gold.", 11, "Fonte de Alimentação 750W", 28 },
                    { new Guid("d9e7f3a6-7b4c-4d8e-9a6b-4c8b1f0e7d5b"), "Fone de ouvido com som surround 7.1.", 5, "Headset Gamer 7.1", 40 },
                    { new Guid("e3f2a1b9-4c7d-4e6f-b4a3-1d5c8e2f9a8b"), "Superfície otimizada para mouses de alta precisão.", 12, "Mousepad Gamer Extra Grande", 70 },
                    { new Guid("e6f5a4b1-8c5d-4c7f-a98c-5d9c2e1f8b6a"), "Cadeira ergonômica para longas sessões de jogos.", 6, "Cadeira Gamer Confort", 25 },
                    { new Guid("f2a1b8c9-9d6e-4b5a-b8a7-6e0d3f2a9c7b"), "Webcam para streaming e videoconferências.", 7, "Webcam Full HD 1080p", 60 },
                    { new Guid("f4a2b1d1-72f5-4e26-9e8f-0c8a9d6b3c7e"), "Laptop de alta performance para jogos.", 1, "Laptop Gamer Pro", 15 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
