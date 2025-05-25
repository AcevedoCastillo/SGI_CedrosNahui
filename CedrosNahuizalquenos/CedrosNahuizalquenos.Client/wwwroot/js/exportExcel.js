MYAPP = {
        Download: function descargarArchivoExcel(nombreArchivo, contenidoBase64) {
        const link = document.createElement('a');
        link.download = nombreArchivo;
        link.href = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + contenidoBase64;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }
}