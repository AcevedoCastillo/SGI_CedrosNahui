
# 🛠️ CedrosNahuizalquenos SGI

Sistema de Gestión Interno para la empresa **Cedros Nahuizalqueños**, enfocado en mejorar la administración de pedidos, productos, clientes y procesos internos.

> 🧱 Proyecto construido con **.NET 9**, **Blazor WebAssembly Hosted**, **MudBlazor** y **Entity Framework Core**.

---

## 📁 Estructura del Proyecto

```
CedrosNahuizalquenos/
├── CedrosNahuizalquenos.sln        # Solución principal
├── CedrosNahuizalquenos.Client/                            # Blazor WebAssembly (Frontend)
├── CedrosNahuizalquenos/                            # ASP.NET Core Web API + EF Core
```

---

## 🚀 Tecnologías Utilizadas

| Tecnología              | Rol                           |
|-------------------------|-------------------------------|
| .NET 9                  | Framework base                |
| Blazor WebAssembly      | Frontend SPA                  |
| ASP.NET Core Web API    | Backend API (hosted)          |
| Entity Framework Core   | Acceso a datos (SQL Server)   |
| SQL Server              | Base de datos relacional      |
| MudBlazor               | UI Component Library          |

---

## 🔧 Instalación y Configuración

1. **Clona el repositorio**
   ```bash
   git clone https://github.com/AcevedoCastillo/SGI_CedrosNahui.git
   cd CedrosNahuizalquenosSGI
   ```

2. **Instala los paquetes**
   ```bash
   dotnet restore
   ```

3. **Configura la conexión a base de datos**
   En `Server/appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TU_SERVIDOR;Database=CedrosNahuizalquenosSGI;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
---

## 🧠 Arquitectura

Se implementa una **arquitectura limpia por capas**, separando responsabilidades:

- `Client`: UI con Blazor WASM + MudBlazor
- `Server`: Controladores API, EF Core, lógica de negocio

---


## 🗃️ Tablas Incluidas en SQL Server

- `Usuarios` → Registro de empleados y administradores
- `Clientes` → Información de clientes
- `Productos` → Catálogo de muebles
- `Pedidos` → Pedidos realizados
- `DetallePedido` → Relación Pedido - Productos
- `Facturas` → Emisión de comprobantes

---

## ✨ Funcionalidades Planeadas

- [x] Gestión de usuarios con roles
- [x] Registro y visualización de productos
- [x] Carrito de compras y pedidos personalizados
- [x] Generación de facturas

---

## ⚠️ Notas Técnicas

- MudBlazor está instalado actualmente **solo en el proyecto Server**.  
  > 🟡 *Se recomienda moverlo al Client si la UI se construirá con componentes MudBlazor.*
- El proyecto es **hosted**, lo que significa que el Server también **sirve la app WebAssembly**.
- Todo está optimizado para desplegar como **una sola unidad** en la nube.

---

## 🐙 Control de Versiones con Gitmojis

Utiliza `gitmoji` para mantener un historial significativo:

| Emoji  | Tipo de cambio        |
|--------|------------------------|
| 🎨     | Mejoras en estilos/UI  |
| ✨     | Nuevas funcionalidades |
| 🐛     | Corrección de errores  |
| ♻️     | Refactorización        |
| 🔧     | Configuración del sistema |
| 📝     | Cambios en documentación |

Ejemplo:
```bash
git commit -m "✨ Agregar pantalla de creación de pedidos"
```

---