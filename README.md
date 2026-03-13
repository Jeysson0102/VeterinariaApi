# API de Gestión de Citas - Clínica Veterinaria 🐾

Esta es una API RESTful desarrollada en **.NET Core (C#)** para gestionar los registros médicos y citas de una clínica veterinaria. Cumple con principios de diseño orientado a objetos, uso de DTOs, operaciones CRUD y estándares profesionales de respuestas HTTP.

## 🚀 Tecnologías y Arquitectura
* **Framework:** .NET Core / ASP.NET Core Web API
* **Lenguaje:** C# 11
* **Arquitectura:** N-Capas (Models, DTOs, Controllers, Middlewares)
* **Almacenamiento:** Persistencia local en memoria (`DataStore` estático centralizado)
* **Diseño:** Orientado a Objetos (Abstracción y Herencia con `BaseEntity`)

## ⚙️ Instrucciones de Ejecución
1. Clona este repositorio en tu máquina local.
2. Abre una terminal y navega hasta la carpeta raíz del proyecto (`VeterinariaApi`).
3. Ejecuta el comando para iniciar el servidor:
   ```bash
   dotnet run
La API se ejecutará por defecto en http://localhost:5225.

## 📌 Endpoints Disponibles
Todas las respuestas de la API están envueltas en un objeto estándar ApiResponse<T> que incluye el estado de éxito, un mensaje descriptivo y los datos solicitados.

## 🐶 Mascotas (/api/mascotas)
GET /api/mascotas - Lista todas las mascotas registradas.

GET /api/mascotas/{id} - Busca una mascota específica por su ID.

POST /api/mascotas - Registra una nueva mascota (Recibe MascotaDTO).

PUT /api/mascotas/{id} - Actualiza los datos de una mascota existente.

DELETE /api/mascotas/{id} - Elimina una mascota del sistema.

## 👨‍⚕️ Veterinarios (/api/veterinarios)
GET /api/veterinarios - Lista todos los veterinarios registrados.

GET /api/veterinarios/{id} - Busca un veterinario por su ID.

POST /api/veterinarios - Registra un nuevo veterinario (Recibe VeterinarioDTO).

PUT /api/veterinarios/{id} - Actualiza los datos de un veterinario.

DELETE /api/veterinarios/{id} - Elimina un veterinario del sistema.

## 📅 Citas (/api/citas)
GET /api/citas - Lista todas las citas médicas.

GET /api/citas/{id} - Busca una cita específica por su ID.

GET /api/citas/mascota/{mascotaId} - [Ruta Jerárquica] Busca todas las citas asociadas a una mascota específica.

POST /api/citas - Registra una cita médica (Recibe CitaDTO con validación de IDs existentes).

PUT /api/citas/{id} - Actualiza los datos de una cita.

DELETE /api/citas/{id} - Cancela/Elimina una cita.

## 🛠️ Buenas Prácticas Implementadas
Respuestas Estandarizadas: Uso de un modelo genérico ApiResponse<T> para garantizar que todos los endpoints devuelvan la misma estructura JSON.

Manejo Centralizado de Excepciones: Implementación de un ExceptionMiddleware que atrapa errores no controlados y devuelve un JSON estandarizado (Status 500) en lugar de romper el servidor o exponer código sensible.

Diseño Orientado a Objetos: Uso de la clase abstracta BaseEntity para aplicar herencia y estandarizar el identificador principal (Id) en todos los modelos.

Uso de DTOs: Separación estricta entre las entidades de dominio y los objetos de transferencia de datos expuestos al cliente.

Validaciones Robustas: Implementación de Data Annotations ([Required], [StringLength]) y el modificador required para garantizar la integridad de la información.

## 📝 Ejemplos de Peticiones (Payloads)
Crear una Mascota (POST /api/mascotas)
JSON
{
  "nombre": "Max",
  "especie": "Perro"
}
Crear una Cita (POST /api/citas)
JSON
{
  "fecha": "2024-05-20T10:30:00Z",
  "motivo": "Vacunación anual",
  "mascotaId": 1,
  "veterinarioId": 1
}

## 🧪 Pruebas de la API
Se recomienda el uso de Postman o Insomnia para probar los endpoints.

Abre Postman o Insomnia.

Crea una nueva petición (GET, POST, PUT, DELETE según corresponda).

Utiliza la URL de tu entorno local (ej. http://localhost:5225/api/mascotas).

Para métodos POST o PUT, ve a la pestaña Body, selecciona raw y el formato JSON, luego pega uno de los ejemplos de payloads mostrados arriba.

Gracias al Global Exception Handler y al patrón ApiResponse, incluso si envías datos malformados o buscas un ID que no existe, siempre recibirás una respuesta JSON clara y estructurada.