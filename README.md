Mirror Api Poc
==============


- Autenticacion
  - La autenticacion es por medio de OAuth 2.0
  - Cuando el usuario accede a la aplicacion por primera vez, se le piden permisos para poder acceder a su cuenta de   Google Glass con ciertos permisos en especial
    - Estos permisos pueden ser 
      - https://www.googleapis.com/auth/glass.timeline
      - https://www.googleapis.com/auth/userinfo.profile
    - El resultado de este paso implica la obtencion de un 'uthorization code', el cual se utilizara en los siguientes pasos. Cabe destacar que este codigo solamente puede ser usado una sola vez
  - Luego, la aplicacion utilizando el 'authorization code', obtiene un 'access token' para poder realizar diversas operaciones durante un tiempo limitado
    - Dado que nosotros necesitaremos acceso de forma 'offline', la primera vez que se realiza este intercambio se obtiene un 'refresh token', el cual sera usado para obtener un nuevo 'access token' sin tener que utilizar un 'authorization code '

