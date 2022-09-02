using AlunoWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlunoWeb.Controllers
{
    public class AlunoController : Controller
    {
        RepositorioAluno repositorio = new();
        public IActionResult Index(string ConteudoPesquisa)
        {
            var alumoVM = new AlunoViewModel
            {
                Alunos = repositorio.BuscarTodosAlunos().ToList()
            };

            if (!string.IsNullOrEmpty(ConteudoPesquisa)) {
                //string matriculaPesquisa = ConteudoPesquisa;
                List<Aluno> alunos;
                try
                {
                    int inteiro = Convert.ToInt32(ConteudoPesquisa);
                    alunos = repositorio.ConsultarContendoMatricula(inteiro).ToList();
                }
                catch (Exception)
                {
                    alunos = repositorio.ConsultarContendoNome(ConteudoPesquisa).ToList();
                }
                alumoVM.Alunos = alunos;
                return View(alumoVM);
            }
  
            return View(alumoVM);
        }
        public IActionResult Adicionar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(int matricula, string AlunoNome, EnumeradorSexo sexo, DateTime dataNascimento, string cpf)
        {
            Aluno aluno = new(matricula, AlunoNome, sexo, dataNascimento);
            if (cpf == null)
            {
                aluno.CPF = cpf;
            }
            else
            {
                aluno.CPF = (CPF)cpf;
            }
            if (Utilitarios.ValidarAluno(aluno,repositorio))
            {
                repositorio.AdicionaAluno(aluno);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public async Task<IActionResult> Excluir(int id)
        {
            var aluno = repositorio.BucarPorId(id);
            return View(aluno);
        }
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirConfirmar(int id)
        {
            var aluno = repositorio.BucarPorId(id);
            repositorio.RemoveAluno(aluno);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var aluno = repositorio.BucarPorId(id);

            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int matricula, string AlunoNome, EnumeradorSexo sexo, DateTime dataNascimento, string cpf)
        {

            Aluno aluno = new(matricula, AlunoNome, sexo, dataNascimento);
            aluno.CPF = cpf;

            repositorio.AlterarAluno(aluno);
            
            return RedirectToAction(nameof(Index));
            return View();
        }

    }
}
