﻿// Uma classe concreta para representar um sistema de hotel, que depende de uma abstração ISuiteRepository para gerenciar as suítes

using testeSuites.Entities.Interfaces;

namespace testeSuites.Entities
{
    public class HotelSystem
    {
        // Um campo privado para armazenar a referência ao repositório de suítes
        private readonly ISuiteRepository repository;

        // O construtor da classe, que recebe uma instância de ISuiteRepository como parâmetro e atribui ao campo privado
        public HotelSystem(ISuiteRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // Um método público para criar e adicionar uma nova suíte ao repositório, recebendo o tipo e o número da suíte como parâmetros
        public void CreateSuite(string type, int number)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            // Usando um switch para criar uma instância da classe correspondente ao tipo da suíte
            ISuite suite = type.ToLower() switch
            {
                "simples" => new SimpleSuite(number),
                "luxo" => new LuxurySuite(number),
                "presidencial" => new PresidentialSuite(number),
                _ => throw new ArgumentException($"O tipo {type} não é válido.")
            };

            // Usando o método Add do repositório para adicionar a suíte criada
            repository.Add(suite);
        }

        // Um método público para remover uma suíte existente do repositório, recebendo o número da suíte como parâmetro
        public void DeleteSuite(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            // Usando o método GetByNumber do repositório para obter a suíte pelo seu número
            ISuite suite = repository.GetByNumber(number) ?? throw new ArgumentException($"A suíte {number} não existe no repositório.");

            // Usando o método Remove do repositório para remover a suíte obtida
            repository.Remove(suite);
        }

        // Um método público para reservar uma suíte existente, recebendo o número da suíte como parâmetro
        public void ReserveSuite(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            // Usando o método GetByNumber do repositório para obter a suíte pelo seu número
            ISuite suite = repository.GetByNumber(number) ?? throw new ArgumentException($"A suíte {number} não existe no repositório.");

            // Usando o método Reserve da suíte para reservá-la
            suite.Reserve();
        }

        // Um método público para cancelar a reserva de uma suíte existente, recebendo o número da suíte como parâmetro
        public void CancelReservation(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            // Usando o método GetByNumber do repositório para obter a suíte pelo seu número
            ISuite suite = repository.GetByNumber(number) ?? throw new ArgumentException($"A suíte {number} não existe no repositório.");

            // Usando o método Cancel da suíte para cancelar a reserva
            suite.Cancel();
        }

        // Um método público para ocupar uma suíte existente, recebendo o número da suíte como parâmetro
        public void OccupySuite(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            // Usando o método GetByNumber do repositório para obter a suíte pelo seu número
            ISuite suite = repository.GetByNumber(number) ?? throw new ArgumentException($"A suíte {number} não existe no repositório.");

            // Usando o método Occupy da suíte para ocupá-la
            suite.Occupy();
        }

        // Um método público para liberar uma suíte existente, recebendo o número da suíte como parâmetro
        public void ReleaseSuite(int number)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            // Usando o método GetByNumber do repositório para obter a suíte pelo seu número
            ISuite suite = repository.GetByNumber(number) ?? throw new ArgumentException($"A suíte {number} não existe no repositório.");

            // Usando o método Release da suíte para liberá-la
            suite.Release();
        }

        // Um método público para mostrar os status de todas as suítes do repositório
        public void ShowStatus()
        {
            // Usando o método GetAll do repositório para obter todas as suítes
            IEnumerable<ISuite> suites = repository.GetAll();

            // Usando um foreach para iterar sobre as suítes e mostrar o seu número, tipo e status
            foreach (ISuite suite in suites)
            {
                string type = suite switch
                {
                    SimpleSuite _ => "simples",
                    LuxurySuite _ => "luxo",
                    PresidentialSuite _ => "presidencial",
                    _ => "desconhecido"
                };

                Console.WriteLine($"Suíte {suite.Number}, tipo {type}, status {suite.Status}");
            }
        }
    }
}