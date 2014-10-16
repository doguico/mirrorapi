#MIRROR API POC


## Autenticacion
  - La autenticacion es por medio de OAuth 2.0
  - Cuando el usuario accede a la aplicacion por primera vez, se le piden permisos para poder acceder a su cuenta de Google Glass 
    - Estos permisos pueden ser 
      - https://www.googleapis.com/auth/glass.timeline
      - https://www.googleapis.com/auth/userinfo.profile
    - El resultado de este paso implica la obtencion de un **authorization code**, el cual se utilizara en los siguientes pasos. Cabe destacar que este codigo solamente puede ser usado una sola vez
  - Luego, la aplicacion utilizando el **authorization code**, obtiene un **access token** para poder realizar diversas operaciones durante un tiempo limitado
    - Dado que nosotros necesitaremos acceso de forma "offline", la primera vez que se realiza este intercambio se obtiene un **refresh token**, el cual sera usado para obtener un nuevo 'access token' sin tener que utilizar un **authorization code**

## Posible Workflow
  - El usuario accede a nuestra aplicacion y se crea una cuenta.
    - Como parte de la creacion de la cuenta nos suministra su email o nos da permisos para acceder al mismo mediante la API de Google
    - Ademas se le pediran permisos para acceder a su timeline de forma offline
  - Se almacena esta informacion, **access token** y **refresh token**, y se indexan contra el email u otro id que permita identificar al usuario.
  - Cada vez que se necesite insertar una static card, enviar un attachment, etc al usuario se consulta por email su **access token** y, en caso de haber expirado, se solicita otra utilizando el **refresh token**.

### Authorization code
  - Para poder obtener el authorization code es necesario pegarle al siguiente endpoint con los siguientes query strings 
    - Endpoint: https://accounts.google.com/o/oauth2/auth
    - Parametros
      - response_type=code
      - client_id= clientId
      - redirect_uri=http://example.com/oauth2callback
      - scope=https://www.googleapis.com/auth/glass.timeline+https://www.googleapis.com/auth/userinfo.profile
      - access_type=offline (se obtiene un nuevo refresh token)
      - approval_prompt=force

##Subscriptions
- El scope debe ser https://www.googleapis.com/auth/glass.timeline
  - Parametros para la subscripcion
    - Requeridos
      - callbackUrl: La URL en la cual las notificaciones seran enviadas. **Debe comenzar con https!**
      - collection: La colección a subscribirse 
        - timeline	
	- location
    - Opcionales
      - operation[]
      - userToken
      - verifyToken

##Location
 - Para poder usar la API de Locations en necesario **subscribirse a las notificaciones**
   - El flujo es el siguiente	
     - El Glassware recibe una actualizacion de la location
     - Se le envia un request al endpoint "glass.locations.get" para obtener la ultima ubicacion conocida.
     - **Notas** 
       1. El Glassware recibe actualizaciones de la location cada **10 minutos**
       2. Para poder pedir la location en el paso de autenticacion se tuvo que haber pedido por el siguiente scope: 
          - https://www.googleapis.com/auth/glass.location
