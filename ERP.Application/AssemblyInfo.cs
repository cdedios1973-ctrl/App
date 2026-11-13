
/// <summary>
/// Creacion de confianza entre ensamblados
/// </summary>

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ERP.View.Presenter")]
[assembly: InternalsVisibleTo("ERP.Infrastructure.Common")]
[assembly: InternalsVisibleTo("ERP.Infrastructure.Sqlite")]
//[assembly: InternalsVisibleTo("ERP.Views.WinForms.UI, PublicKey= numeros...")]
//[assembly: InternalsVisibleTo("ERP.Tests")]
