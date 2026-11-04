
📁 MiApp.Solution
│
├── 🖥️ Presentation (WebAPI / Controllers)
│   └── 🔗 Referencia a: Application, Domain
│
├── ⚙️ Application
│   ├── 📂 UseCases / Services (Lógica del ERP)
│   ├── 📂 Mappers (POCO ⇄ DTO)
│   └── 🔗 Referencia a: Domain, Infrastructure
│
├── 📦 Infrastructure
│   ├── 📂 Persistence (Contexto BD, Repositorios)
│   ├── 📂 Entities (Modelos POCO)
│   └── 🔒 Visibilidad: Solo accesible desde Application
│
└── 💎 Domain
    ├── 📂 DTOs (Data Transfer Objects)
    ├── 📂 Enums & Interfaces globales
    └── 🌍 Visibilidad: Proyecto base (Todos lo referencian)
