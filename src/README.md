# Docker Notes


### build single container
open terminal in /src folder of this repo
```powershell
docker build -t <author>/<app-name>:<tag> -f <path-dockerfile> .
```

Example:
```powershell
docker build -t dragwar/gcprazorpagesapp:latest -f .\GCP.RazorPagesApp\Dockerfile . 
```



### test env file configuration for compose file
open terminal in /src folder of this repo
```powershell
docker-compose --env-file <env-file-path> config
```

Example:
```powershell
docker-compose --env-file .\.env.dev config
```

Outputs:
```yml
services:
  db:
    container_name: db
    environment:
      POSTGRES_PASSWORD: password
      POSTGRES_USER: postgres
    image: postgres
    networks:
      backend: null
    ports:
    - mode: ingress
      target: 5432
      published: 9002
      protocol: tcp
    volumes:
    - type: volume
      source: gcp_db_data
      target: /var/lib/postgresql/data
      volume: {}
  web:
    build:
      context: C:\code\Dragwar\GCP\src
      dockerfile: ./GCP.RazorPagesApp/Dockerfile
      args:
        buildConfiguration: Debug
    container_name: web
    depends_on:
      db:
        condition: service_started
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      CONNECTIONSTRINGS__GCPCONTEXT: Server=db;Port=5432;Database=GCP;User Id=postgres;Password=password
      SEEDEROPTIONS__DATABASEMIGRATIONOPTIONS__0: migrate
      SEEDEROPTIONS__SKIPSEED: "false"
    links:
    - db
    networks:
      backend: null
    ports:
    - mode: ingress
      target: 80
      published: 9001
      protocol: tcp
networks:
  backend:
    name: src_backend
    driver: bridge
volumes:
  gcp_db_data:
    name: GCP_DB_DATA_DEV
```


### build and run multiple containers at once
open terminal in /src folder of this repo
```powershell
docker-compose --env-file <env-file-path> up --build
```

Example:
```powershell
docker-compose --env-file .\.env.dev up --build
```


### tear down multiple containers at once
open terminal in /src folder of this repo
```powershell
docker-compose --env-file <env-file-path> down
```

Example:
```powershell
docker-compose --env-file .\.env.dev down
```
