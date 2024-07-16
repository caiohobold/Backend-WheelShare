using EmprestimosAPI.Models;

namespace EmprestimosAPI.DTO.Equipamento
{
    public class EquipamentoUpdateDTO
    {
        public string NomeEquipamento { get; set; }
        public int EstadoEquipamento { get; set; }
        public int CargaEquipamento { get; set; }
        public string DescricaoEquipamento { get; set; }
        public IFormFile Foto1 { get; set; }
        public IFormFile Foto2 { get; set; }
        public int IdLocal { get; set; }
        public int idAssociacao { get; set; }

    }
}
