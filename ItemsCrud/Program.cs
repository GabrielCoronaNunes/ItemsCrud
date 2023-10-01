using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ItemsCrud
{
    internal class Program
    {
        const string CONNECTION_STRING = "Data Source = DESKTOP-10JQR2T\\SQLEXPRESS; Initial Catalog=crudItems; Integrated Security=true";

        static void Main()
        {
            var menu = new StringBuilder("Digite a opção que deseja fazer:")
                .AppendLine("1) Cadastrar um item")
                .AppendLine("2) Remover um item")
                .AppendLine("3) Editar um item")
                .AppendLine("4) Visualizar todos os itens")
                .AppendLine("5) Sair")
                .ToString();

            while (true)
            {
                Console.WriteLine(menu);

                _ = int.TryParse(Console.ReadLine(), out var option);

                switch (option)
                {
                    case 1: Create(); break;
                    case 2: Delete(); break;
                    case 3: Update(); break;
                    case 4: Read(); break;
                    case 5: Console.WriteLine("Encerrando o programa."); return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
            }
        }

        private static void Read()
        {
            string query = "SELECT * FROM items";
            SqlConnection connection = new(CONNECTION_STRING);
            SqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Items registrados:");
                while (reader.Read())
                {
                    Console.WriteLine($"Nome: {reader["Nome"]} Id:{reader["id"]}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao recuperar nomes: " + ex.Message);
            }
        }

        private static void Update()
        {
            Console.WriteLine("Digite o ID do item que deseja editar:");
            int itemId = int.Parse(Console.ReadLine());
            Console.WriteLine("Digite o novo nome para o item:");
            string? newItemName = Console.ReadLine();
            string strSql = "UPDATE items SET Nome = @Nome WHERE id = @Id";
            using SqlConnection connection = new(CONNECTION_STRING);
            SqlCommand command = new(strSql, connection);
            command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = newItemName;
            command.Parameters.Add("@Id", SqlDbType.Int).Value = itemId;

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Item atualizado com sucesso.");
                }
                else
                {
                    Console.WriteLine("Nenhum item foi atualizado. Verifique o ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar item: " + ex.Message);
            }
        }

        private static void Delete()
        {
            Console.WriteLine("Digite o ID do item que deseja remover:");
            int itemIdToRemove = int.Parse(Console.ReadLine());
            string strSql = "DELETE FROM items WHERE id = @Id";
            using SqlConnection connection = new(CONNECTION_STRING);
            SqlCommand command = new(strSql, connection);
            command.Parameters.Add("@Id", SqlDbType.Int).Value = itemIdToRemove;

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Item removido com sucesso.");
                }
                else
                {
                    Console.WriteLine("Nenhum item foi removido. Verifique o ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao remover item: " + ex.Message);
            }
        }

        private static void Create()
        {
            string strSql = "INSERT INTO items(Nome) values (@Nome)";
            using SqlConnection connection = new(CONNECTION_STRING);
            SqlCommand command = new(strSql, connection);
            Console.WriteLine("Digite o item que deseja adicionar");
            string? itemNameInput = Console.ReadLine();
            command.Parameters.Add("Nome", SqlDbType.VarChar).Value = itemNameInput;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Cadastro realizado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}