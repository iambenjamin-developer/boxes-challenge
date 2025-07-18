# BoxesChallenge

## Diagrama

```mermaid
flowchart TD
    A[Cliente] -->|POST /api/leads| B[LeadsController]
    B --> C[Validar campos requeridos]
    C -->|OK| D[Consultar API externa workshops]
    C -->|Error| F[400 Bad Request]

    D -->|place_id válido| E[Guardar en memoria / EF InMemory]
    D -->|place_id inválido| F[422 Unprocessable Entity]

    E --> G[201 Created + Lead registrado]
```


Modelo request

```
{
  "place_id": 2222,
  "appointment_at": "2023-10-01T10:00:00Z",
  "service_type": "cambio_aceite",
  "contact": {
    "name": "Juan Pérez",
    "email": "juan@mail.com",
    "phone": "12345678"
  },
  "vehicle": {
    "make": "Toyota",
    "model": "Corolla",
    "year": 2020,
    "license_plate": "ABC123"
  }
}

```