1) Install .Net Core 3.x Runtime
2) Open up command prompt 
3) Go to the solution directory (where the .sln lives)
4) Run  'docker build -t poke .' command
5) Run 'docker run -d -p 8080:80 --name myapp poke' command
6) Open up browser and enter http://localhost:8080/Pokemon/ditto or http://localhost:8080/Pokemon/translated/ditto
