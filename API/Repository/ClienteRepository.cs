using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.DTO;
using API.Models;

namespace API.Repository
{
    public class ClienteRepository
    {
        private readonly VendasContext _context;

        public ClienteRepository(VendasContext context)
        {
            _context = context;
        }

        public void Cadastrar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public bool EfetuarLogin(LoginClienteDTO dto)
        {
            var login = false;

            foreach (var cliente in _context.Clientes)
            {
                if (cliente.Login == dto.Login && cliente.Senha == dto.Senha)
                {
                    login = true;
                }
            }

            return login;
        }

        public List<ListarClientesDTO> ListarClientes()
        {
            var clientes = _context.Clientes.Select(x => new ListarClientesDTO(x))
                                            .ToList();
            return clientes;
        }

        public Cliente ObterPorId(int id)
        {
            var cliente = _context.Clientes.Find(id);
            return cliente;
        }

        public List<ObterClienteDTO> ObterPorNome(string nome)
        {
            var clientes = _context.Clientes.Where(x => x.Nome.Contains(nome))
                                            .Select(x => new ObterClienteDTO(x))
                                            .ToList();
            
            return clientes;
        }

        public Cliente AtualizarCliente(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public void DeletarCliente(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
            _context.SaveChanges();
        }

        public void AtualizarSenha(Cliente cliente, AtualizarSenhaClienteDTO dto)
        {
            cliente.Senha = dto.Senha;
            AtualizarCliente(cliente);
        }
    }
}