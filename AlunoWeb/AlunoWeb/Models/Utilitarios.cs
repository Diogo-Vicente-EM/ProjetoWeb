using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlunoWeb.Models
{
    public abstract class Utilitarios
    {
        public static bool ValidaCPF(string cpf)
        {
            string valor = cpf.Replace(".", "");

            valor = valor.Replace("-", "");
            valor = valor.Replace(" ", "");

            if (valor.Length != 11)

                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)

                if (valor[i] != valor[0])

                    igual = false;

            if (igual || valor == "12345678909")

                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                try
                {
                    numeros[i] = int.Parse(
                      valor[i].ToString());
                }
                catch (FormatException)
                {

                    return false;
                }
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)

            {
                if (numeros[10] != 0)

                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;

        }
        public static string FormatCPF(string CPF)
        {
            CPF = CPF.Replace(".", "").Replace("-", "").Replace(" ", "").Replace("/", "");

            return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }
        public static bool ValidarAluno(Aluno aluno,RepositorioAluno repositorio)
        {
            try
            {
                DateTime dataAtual = DateTime.Now;
                if (aluno.DataNascimento.Year < 1850)
                {
                    return false;
                }
                if (aluno.DataNascimento > dataAtual)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            

            return true;
        }

        public static bool ValidarMatricula(string matricula)
        {
            if (matricula == string.Empty)
            {            
                return false;
            }
            int matriculaInteira = Convert.ToInt32(matricula);
            if (matriculaInteira == 0)
            {          
                return false;
            }

            return true;
        }
        public static bool ValidarNome(string nome)
        {

            if (nome == string.Empty)
            {
                return false;
            }
            return true;

        }
        public static bool ValidarSexo(string sexo)
        {
            if (sexo == string.Empty)
            {
             
                return false;
            }
            return true;
        }
        public static bool ValidarNascimento(string data)
        {

            try
            {
                DateTime dataAtual = DateTime.Now;
                DateTime nascimento = Convert.ToDateTime(data);
                if (nascimento.Year < 1850)
                {
            

                    return false;
                }
                if (nascimento > dataAtual)
                {
                
                    return false;
                }
            }
            catch (Exception)
            {
              
                return false;
            }
            return true;
        }

    }
}
