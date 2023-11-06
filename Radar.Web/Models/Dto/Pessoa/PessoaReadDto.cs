﻿namespace Radar.Web.Models.Dto;

public class PessoaReadDto
{
    public int PessoaId { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string SenhaHash { get; set; } = null!;
    public string SenhaKey { get; set; } = null!;
    public string? Descricao { get; set; }
    public DateTime DataNascimento { get; set; }
}