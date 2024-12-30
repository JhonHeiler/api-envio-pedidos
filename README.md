# API REST - Servicio de Abastecimiento ACME

## Descripción General
Este proyecto implementa una API REST en .NET 8 que recibe pedidos de la tienda de carrera 70 de ACME. La API transforma los mensajes JSON a XML (SOAP) y los envía a un endpoint externo. Posteriormente, convierte la respuesta SOAP a formato JSON y la retorna al cliente.

## Requisitos
- **Visual Studio Code**
- **.NET SDK 8.0.404 (x64)**
- **Docker**
- **Git**

## Instalación y Configuración
1. **Clonar el repositorio:**
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd <NOMBRE_DEL_PROYECTO>
   ```

2. **Restaurar dependencias:**
   ```bash
   dotnet restore
   ```

3. **Construir el proyecto:**
   ```bash
   dotnet build
   ```

4. **Ejecutar la API:**
   ```bash
   dotnet run
   ```

## Contenedor Docker
Para ejecutar el proyecto en Docker:

1. **Construir la imagen Docker:**
   ```bash
   docker build -t acme-api .
   ```

2. **Ejecutar el contenedor:**
   ```bash
   docker run -p 8080:8080 acme-api
   ```

3. **Acceder a la API:**
   ```
   http://localhost:8080/api/pedidos
   ```

## Endpoints
### POST /api/pedidos
- **Descripción:** Recibe pedidos en formato JSON y los transforma a XML (SOAP) para enviarlos a un sistema externo.
- **Ejemplo de Petición (JSON):**
   ```json
   {
     "enviarPedido": {
       "numPedido": "75630275",
       "cantidadPedido": "1",
       "codigoEAN": "00110000765191002104587",
       "nombreProducto": "Armario INVAL",
       "numDocumento": "1113987400",
       "direccion": "CR 72B 45 12 APT 301"
     }
   }
   ```

- **Respuesta (JSON):**
   ```json
   {
     "enviarPedidoRespuesta": {
       "codigoEnvio": "80375472",
       "estado": "Entregado exitosamente al cliente"
     }
   }
   ```

## Mapeo de Datos
### JSON a XML (SOAP)
| REST (JSON)       | SOAP (XML)     | Ejemplo        |
|------------------|----------------|----------------|
| numPedido        | pedido         | 75630275       |
| cantidadPedido   | Cantidad       | 1              |
| codigoEAN        | EAN            | 00110000765191002104587 |
| nombreProducto   | Producto       | Armario INVAL  |
| numDocumento     | Cedula         | 1113987400     |
| direccion        | Direccion      | CR 72B 45 12 APT 301 |

### XML (SOAP) a JSON
| SOAP (XML)     | REST (JSON)       | Ejemplo                        |
|----------------|-------------------|--------------------------------|
| Codigo         | codigoEnvio       | 80375472                       |
| Mensaje        | estado            | Entregado exitosamente al cliente |

## Ejemplo de Respuesta XML
```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:env="http://WSDLs/EnvioPedidos/EnvioPedidosAcme">
   <soapenv:Header/>
   <soapenv:Body>
      <env:EnvioPedidoAcmeResponse>
         <EnvioPedidoResponse>
            <Codigo>80375472</Codigo>
            <Mensaje>Entregado exitosamente al cliente</Mensaje>
         </EnvioPedidoResponse>
      </env:EnvioPedidoAcmeResponse>
   </soapenv:Body>
</soapenv:Envelope>
```

## Estructura del Proyecto
```
ApiEnvioPedidos/
├── Controllers/
│   └── PedidosController.cs
├── Business/
│   └── PedidoService.cs
├── Services/
│   ├── SoapClient.cs
│   └── TransformService.cs
├── Models/
│   ├── PedidoRequest.cs
│   └── PedidoResponse.cs
├── Program.cs
├── Dockerfile
└── ApiEnvioPedidos.csproj
```


