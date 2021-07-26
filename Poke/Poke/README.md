1)Open up command prompt 
2) Go to the solution directory (where the .sln lives)
3) Run  'docker build -t poke .' command
4) Run 'docker run -d -p 8080:80 --name myapp poke' command
5) Open up browser and enter http://localhost:8080/Pokemon/ditto or http://localhost:8080/Pokemon/translated/ditto