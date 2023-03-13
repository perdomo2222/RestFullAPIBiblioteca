using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using Dominio.Entidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Feautres.Libros.Consultas.ObtenerLibroPorID
{
    public class ObtenerLibroId : IRequest<Respuesta<Librodto>>
    {
        public int Id { get; set; }

        public class ObtenerLibroHandler : IRequestHandler<ObtenerLibroId, Respuesta<Librodto>>
            {
            private readonly IRepositorioAsincrono<Libro>  _repositorioAsincrono;
            private readonly IMapper _mapper;

            public ObtenerLibroHandler(IRepositorioAsincrono<Libro> repositorioAsincrono, IMapper mapper)
            {
                _repositorioAsincrono = repositorioAsincrono;
                _mapper = mapper;
            }


            public async Task<Respuesta<Librodto>> Handle(ObtenerLibroId request, CancellationToken cancellationToken)
            {
                var libro = await _repositorioAsincrono.GetByIdAsync(request.Id);
                if (libro == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id{request.Id}");
                }
                else
                {
                    
                    var objetodto = _mapper.Map<Librodto>(libro);

                    return new Respuesta<Librodto>(objetodto);
                }
            }
        }
    }
}
