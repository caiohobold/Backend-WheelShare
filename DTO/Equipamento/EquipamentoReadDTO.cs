namespace EmprestimosAPI.DTO.Equipamento
{
    public class EquipamentoReadDTO
    {
        public int IdEquipamento { get; set; }
        public string NomeEquipamento { get; set; }
        public string NomeCategoriaEquipamento { get; set; }
        public int IdCategoria {  get; set; }
        public int EstadoEquipamento { get; set; }
        public int CargaEquipamento { get; set; }
        public string? DescricaoEquipamento { get; set; }
        public int StatusEquipamento { get; set; }
        public string? Foto1 { get; set; }
        public string? Foto2 { get; set; }
        public int IdLocal { get; set; }
        public string NomeLocal { get; set; }
        public int idAssociacao { get; set; }
    }
}
