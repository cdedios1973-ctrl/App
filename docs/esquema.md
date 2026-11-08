# 📂 Arquitectura de la Solución (TestApp)

Diagrama de dependencias y responsabilidades de los ensamblados en el proyecto.

---

## 📐 Mapa de la Solución

📁 Solución "TestApp"
│
├── 📁 Controllers (Capa de Entrada / Orquestación de UI)
│   └── 🟢 ERP.Views.Controller
│       └── 🔗 Referencia a: ERP.Application, ERP.Domain
│
├── 📁 Core (Reglas y Lógica de Negocio Pura)
│   ├── 🟢 ERP.Application
│   │   ├── 📂 UseCases / Services (Casos de uso del ERP)
│   │   ├── 📂 DTOs / Mappers (Estructuras de datos para la UI)
│   │   └── 🔗 Referencia a: ERP.Domain
│   │
│   └── 🟢 ERP.Domain
│       ├── 📂 Entities (Modelos de negocio / POCOs)
│       ├── 📂 Interfaces (Contratos de persistencia R/W)
│       └── 🌍 Visibilidad: Núcleo aislado (No referencia a nadie)
│
├── 📁 Infrastructure (Detalles Técnicos y Datos Desacoplados)
│   ├── 🟢 ERP.Infrastructure.Common
│   │   └── 📂 Herramientas base y utilitarios genéricos.
│   │
│   ├── 🟢 ERP.Infrastructure.Services
│   │   ├── 📂 Configuración global (Mapeos JSON).
│   │   └── 🔗 Referencia a: ERP.Infrastructure.Common
│   │
│   └── 🟢 ERP.Infrastructure.Sqlite (Persistencia Aislada)
│       ├── 📂 Persistence (Drivers nativos SqLite / ADO.NET)
│       ├── 📂 Read / Write (Consultas e implementaciones concretas)
│       └── 🔗 Referencia a: ERP.Domain, ERP.Infrastructure.Services
│
├── 📁 Views (Interfaz de Usuario)
│   ├── 🟢 ERP.Views.ViewModel.UI
│   └── 🟢 ERP.Views.WinForms.UI
│       └── 🔗 Referencia a: ERP.Views.Controller, ERP.Views.ViewModel.UI
│
└── 📁 docs (Documentación Técnica del Sistema)
    ├── 📁 adr (Architectural Decision Records)
    ├── 📁 research (Investigaciones previas)
    ├── 📝 architecture.md
    └── 📝 esquema.md


[ Capa de Vista / WinForms ] 
         │  (Dispara la acción)
         ▼
[ ERP.Views.Controller ]
         │  (Llama al caso de uso)
         ▼
[ ERP.Application ] 
         │  (Ejecuta la lógica usando interfaces de)
         ▼
[ ERP.Domain (Interfaces R/W) ] 
         ▲
         │  (Inyección de dependencias en tiempo de ejecución)
[ ERP.Infrastructure.Sqlite ]

