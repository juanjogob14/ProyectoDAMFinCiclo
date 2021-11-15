# ProyectoDAMFinCiclo
En base a la necesidad de la conexión del cliente con el servidor, le he dado una vuelta a la conexion general después de encontrar un tutorial en que me quedaba explicado bastante mejor y gracias al cual consigo la conexion de la forma que deseaba.
A parte de eso, con respecto al anterior commit , tenemos ya el envío de mensajes y el borrado del usuario de la base de datos una vez se desconecta del servidor.
Lo siguiente y último será conseguir la lista de usuarios conectados que se muestra en el list box derecho del formulario 2, y una vez conseguido eso, se daría por finalizado el proyecto.

En esta última actualización cambiamos el planteo del proyecto de forma que en vez de dos formularios trabajamos con uno solo, volvemos al código previo al TCP client de modificacion de la semana pasada
en el aspecto del uso de sockets, conseguimos la comunicacion con este metodo de sockets y conseguimos todo lo que m ehbai propuesto para ello, con el trabajo con bases desde el servidor y no desde el cliente.
El problema final fue que a la hora de las comprobaciones de si el usuario está dentro no conseguí mantenerlo en la primera de las pantallas por falta de tiempo, pero no ejecutamos dos clientes con la misma identificación
ni guardamos dos usuarios en la base con el mismo nombre.
