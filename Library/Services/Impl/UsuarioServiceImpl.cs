using AutoMapper;
using Library.Context;
using Library.Domains;
using Library.DTOs.Request.Auth;
using Library.DTOs.Response;
using Library.Repositories;
using System.Security.Claims;

namespace Library.Services.Impl
{
    public class UsuarioServiceImpl : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioServiceImpl(IUsuarioRepository usuarioRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) 
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UsuarioDTO> GetPerfilUsuarioAsync()
        {
            if (_httpContextAccessor.HttpContext.User == null)
            {
                return null;
            }

            var usuarioIdClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var usuarioId = Guid.Parse(usuarioIdClaim.Value);

            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(usuarioId);
            var response = _mapper.Map<UsuarioDTO>(usuario);

            return response;
        }

        public async Task<UsuarioDTO> GetUsuarioByNameAndPasswordAsync(string name, string password)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuarioByNameAndPasswordAsync(name, password);
                if (usuario == null)
                {
                    throw new KeyNotFoundException("[S] Usuario no encontrado");
                }
                var response = _mapper.Map<UsuarioDTO>(usuario);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<UsuarioDTO> CreateAsync(RegisterUsuarioDTO dto)
        {
            try
            {
                if (await ValidateUsuario(dto))
                {
                    var usuario = _mapper.Map<Usuario>(dto);
                    var entity = await _usuarioRepository.AddAsync(usuario);
                    var response = _mapper.Map<UsuarioDTO>(entity);
                    return response;

                } else
                {
                    throw new InvalidOperationException("La validación del usuario ha fallado.");
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception($"[S] Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<UsuarioDTO> UpdateAsync(EditUsuarioDTO dto)
        {
            try
            {
                var dtoId = Guid.Parse(dto.Id);
                var usuario = await _usuarioRepository.GetUsuarioByIdAsync(dtoId);
                if (usuario == null)
                {
                    throw new KeyNotFoundException("[S] Usuario no encontrado");
                }

                _mapper.Map(dto, usuario); // mapeo sin generación de instancia

                var usuarioEditado = await _usuarioRepository.PutAsync(usuario);
                var response = _mapper.Map<UsuarioDTO>(usuarioEditado);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        public async Task<UsuarioDTO> DeleteAsync(Guid id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
                if (usuario == null)
                {
                    throw new KeyNotFoundException("[S] Usuario no encontrado");
                }
                var usuarioEliminado = await _usuarioRepository.DeleteAsync(usuario);
                var response = _mapper.Map<UsuarioDTO>(usuarioEliminado);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("[S] Error " + ex.Message);
            }
        }

        private async Task<bool> ValidateUsuario(RegisterUsuarioDTO dto)
        {
            try
            {
                var emailEnUso = await _usuarioRepository.GetUsuarioByEmailAsync(dto.Email);
                if (emailEnUso)
                {
                    throw new InvalidOperationException($"Ya existe un usuario con el email '{dto.Email}'.");
                }

                var usuarioExistente = await _usuarioRepository.GetUsuarioByNameAndPasswordAsync(dto.Nombre, dto.Password);
                if (usuarioExistente != null)
                {
                    throw new InvalidOperationException($"Ya existe un usuario con ese nombre y contraseña '{dto.Email}'.");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
