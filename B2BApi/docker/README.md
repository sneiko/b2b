# B2B API

##### Deploy by docker

1. Build image

    ```docker build -t aspnetapp . ```
    
2. Run container

    ```docker run -d -p 80:80 --name b2bapi aspnetapp```
    
3. Check server

    ```curl -X POST "http://<HOST>/api/v1/Common/Authorization" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{\"userName\":\"string\",\"password\":\"string\"}"```

##### Новое обновление

1. Stop 

    ```sudo  docker-compose stop```

2. Build 

      ```cd docker``` переходим в директорию 
      
      ```sudo docker-compose build``` собираем проект
3. Start 

      ```sudo docker-compose up -d``` запускаем проект






 