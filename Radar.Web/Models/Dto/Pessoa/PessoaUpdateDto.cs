﻿namespace Radar.Web.Models.Dto;

public class PessoaUpdateDto
{
    public int PessoaId { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string? Descricao { get; set; }
}
