# Code Example

Shows one webapi service calling a gRPC service, within linux containers using docker-compose.

# Prerequisites
- dot net core 3.0, preview 5
- docker
- Visual Studio 16.1, preview 3
- Docker Desktop (on Windows), run using `linux containers`. You'll need to create an account on dockerhub to download the installer.


# Learnings

## dot net project creation
Dotnet can be used to create a `webapi` service using the command 

`dotnet new webapi`

and a `gRPC` service can be created with the command:

`dotnet new grpc`.

## Ways of running the services in Docker for Windows

Reference: https://www.docker.com/docker-desktop/getting-started-for-windows

### Run as separate executables

- Right click `hello-grpc` > `Debug` > `Start New Instance`
- Right click `hello-controller` > `Debug` > `Start New Instance`
- This should open a web browser with the url: `https://localhost:44300/api/values
`
### Create dockerfiles and docker-compose files

- Right click the project `hello-controller` > `Add...` > `Add Orchestrator Support` > `Docker`. Select `linux containers`.

This will create `dockerfile`, `docker-compose.yml`, and `docker-compose.override.yml` files, and a `docker-compose` project.


### Run docker compose from solution

- Right click the `docker-compose` project and set as startup project

- Press `F5` to run services in debug mode.

- Navigate to : https://localhost:44310/api/values

- When finished this method of running, unset `docker-compose` as the startup project (becaues it will automatically keep recreating the containers)

- Delete the containers:
    - `docker container ls`
    - `docker container stop <container-id>`
    - `docker container rm <container-id>`

### Run docker compose from command line

- `cd <directory containing docker-compose.yml>`
- `docker-compose up -d`
- Navigate to : https://localhost:44310/api/values

- Delete the containers:
    - `docker container ls`
    - `docker container stop <container-id>`
    - `docker container rm <container-id>`

### Run in Docker Swarm 

Create a swarm manager
- ` docker swarm init`

Run swarm using docker-compose file
- `docker stack deploy --orchestrator=swarm  --compose-file=docker-compose.yml  hellostack`

Expose http port
- `docker service update --publish-add 12345:80 hellostack_hello-controller`

Navigate to http://localhost:12345/api/values

Shut down stack
- `docker stack rm hellostack`


### Run in kubernetes
Open Docker in system tray > `Settings` > `Kubernetes`
Click `Enable Kubernetes`, and press Apply.

## Create docker images

The following example references the project greeting which is implemented on Windows Containers.

Create a docker image with your `dockerId` (account on dockerhub). Replace `<dockerid>` with the name for your docker hub account.

- `cd <directory containing docker-compose.yml>`
- `docker image build -f greeting-controller/Dockerfile    --tag <dockerId>/greetingcontroller .`

Note the `.` at the end of the line.

- `cd <directory containing docker-compose.yml>`
- `docker image build -f greeting-service/Dockerfile    --tag <dockerId>/greetingservice .`

Note the `.` at the end of the line.

Try running the containers

- `docker container run --detach --publish 8081:80 --name greetingcontroller <dockerId>/greetingcontroller`

- Navigate to `http://localhost:8081/api/values`. Should see a 500 error because the service side is not accessible. However it does show that the controller is operational.

#### Push images to docker hub image repository

The images, properly tagged should be pushed to docker hub.

- `docker image push <dockerId>/greetingcontroller ` 
- `docker image push <dockerId>/greetingservice `

Stop and remove running containers

- `docker image stop greetingcontroller` 
- `docker image rm greetingcontroller` 


## Preparing Windows Server 2019 to run Docker

Instructions on how to run Windows Containers on Windows Server 2019.


## docker networking
Use the default bridge mode provided by docker. A service can be referenced using it's name, e.g. `hello-grpc` in `ValuesController.cs(20)`.

## docker config
There is a file called `docker-compose.override.yaml`, which contains the port mapping for the services.
