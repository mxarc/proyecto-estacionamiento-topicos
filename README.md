# proyecto-estacionamiento-topicos
🚗 Proyecto estacionamiento para la materia de tópicos avanzados de programación

**Descripción general**

La aplicación manejara el cálculo de cuotas de estacionamiento para autos que pagan pensión,
así como para autos que ingresan por horas/días. Se deben de incluir algunos reportes que permitan
administrar correctamente el establecimiento

## Opciones a incluir:
- Alta/Modificación de lugares para estacionarse: Se deben de crear objetos para que se agreguen a una
Colección. La clase base debe de tener una clave del lugar (con 3 digitos), una descripción y un estatus
que indique si está disponible o no. También se podrá modificar la descripción para que se le asigno,
no se debe permitir códigos repetidos.
- Registro de entrada: Al entrar un auto al estacionamiento se debe de registrar dentro de una TablaHash,
los datos que se deben de considerar son: código o num de entrada, matrícula del auto, clave del lugar
asignado (se debe de tomar algún lugar disponible de la colección de lugares, este dato debe de ser
buscado automáticamente sin que el usuario lo proporcione, el estatus del lugar asignado debe de cambiar a
asignado.) También almacenar la hora de entrada (considerando 2 diitos 01, 10, 14, 18 horas), así
como el minuto de entrada (con 2 digitos), así como la fecha de entrada (la fecha puede manejarse con día, mes y año),
se puede utilizar algún tipo de dato que maneje fechas. Antes de registrar la entrada se debe verificar que la
matrícula del auto no se encuentre dentro de las matrículas que pagan pensión (en la tabla Hash de pensiones).
Si se encuentra dentro de esta tabla no se debe de asignar ni guardar en la tabla hash de entrada.
- Salida del estacionamiento: Al salir del auto se debe de buscar el código de entrada (en la TablaHash), para
calcular la cuota a pagar, la primera hora o fracción se debe de pagar a 15 pesos, por cada hora o fracción adionales
se debe cobrar 10 pesos. Si el usuario perdió el ticket de estacionamiento se de utilizar la búsqueda por
matricula para calcular la cuota, agregandole 80 pesos adicionales al cobro normal. Si el auto es pensionado no
se cobrara nada, pero para esto se debe de buscar la matricula dentro de la tabla hash.
- Alta de pensiones: Existen usuarios que podrán pagar una pensión mensual y podrán entrar o salir a cualquier hora,
las pensiones deben de ser registradas dentro de una tabla hash. Para dar de alta a los usuarios que utilicen este servicio
se deberán tomar en cuenta la clave de la matricula, modelo del auto, nombre del propietario, la fecha de vencimiento,
fecha de ingreso, cuota de pago. La cuota de pago se calcula en base a los días que el auto permanecerá, si los días son
menor a 15 se paga 60 pesos por día y si son mayor o igual a 15 se paga a 40 pesos por día. Se debe de presentar por
pantalla la cuota a pagar. Para poder dar de alta el auto no debe de estar registrado. En caso de que se encuentre vencida
ya la pensión, si se permitirá la alta.
- Ingreso/Salida de autos pensionados: Cuando un auto pensionado desee ingresar se deberá buscar la matricula para saber
si la pensión no está vencida o exista el objeto en la tabla de pensiones.
- Consulta de lugares disponibles/ocupados: Se presentaran por pantalla un listado de todos los lugares que el usuario
desee listar o Disponibles u Ocupados.
- Consulta de pensiones: Se presentaran por pantalla todas las pensiones u opcionalmente se pedirá el nombre del propietario
para que se presenten todos los autos que estén asignados a esa persona.