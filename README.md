# ProductService

## Descripción

ProductService es un servicio API que permite la gestión de productos. Proporciona funcionalidades para insertar, actualizar y obtener productos por ID. Además, implementa cache para los estados del producto y registra el tiempo de respuesta de cada solicitud.

## Patrones y Arquitectura
Este proyecto está estructurado siguiendo los principios del diseño DDD (Domain-Driven Design) y adopta la Arquitectura en Cebolla (Onion Architecture), organizando el código en capas claras y separadas:

**Entidades:** Contiene las entidades del dominio, que representan los conceptos fundamentales y las reglas de negocio del sistema.
**Aplicación:** Aquí se encuentra la lógica de la aplicación, implementada en servicios y handlers que manejan las solicitudes del cliente y orquestan las operaciones del sistema.
**Infraestructura:** Gestiona los detalles de implementación, incluyendo la persistencia de datos, servicios externos y cualquier integración con sistemas de terceros.
**API:** Proporciona una interfaz pública para interactuar con la aplicación, exponiendo endpoints que permiten a los clientes consumir los servicios ofrecidos por el sistema.

Esta arquitectura fomenta la modularidad, el bajo acoplamiento y la alta cohesión entre las diferentes capas, facilitando el mantenimiento, la escalabilidad y la testabilidad del código.
- **CQRS**: Command Query Responsibility Segregation.
- **Mediator Pattern**: Usado para manejar comandos y consultas.
- **Repository Pattern**: Abstracción para el acceso a datos.
- **Unit of Work**: Centralización del manejo de la capa de datos.
- **SOLID Principles**: Seguir principios de diseño orientado a objetos.

- Además se maneja el concepto de middlewares para cumplir con requerimientos especificos de la prueba. Tales cómo el manejo de logs y el manejo d eerrores centralizados dentro de la aplicación.

## Tecnologías

- .NET Core 8.0
- Entity Framework Core
- MediatR
- FluentValidation
- Swagger

## Instalación

1. Clona el repositorio:
    ```sh
    git clone https://github.com/tu-repositorio/ProductService.git
    cd ProductService
    ```

2. Configura la base de datos en `appsettings.json`.

3. Restaura las dependencias:
    ```sh
    dotnet restore
    ```
4. Verificar el Endpoint creado como Mock que este funcionando
    ```sh
   https://6671b269e083e62ee43cbbd2.mockapi.io/api/DiscountProduct/
    ```
6. Ejecuta la aplicación:
    ```sh
    dotnet run --project ProductService.Api
    ```

7. Navega a `http://localhost:5000/swagger` para ver la documentación de la API.

## Ejecución de Pruebas

Para ejecutar las pruebas unitarias, usa el siguiente comando:
```sh
dotnet test
