# B2B API

##### Deploy by docker

1. Build image

    ```docker build -t aspnetapp . ```
    
2. Run container

    ```docker run -d -p 80:80 --name b2bapi aspnetapp```
    
3. Check server

    ```curl -X POST "http://<HOST>/api/v1/Common/Authorization" -H "accept: application/json" -H "Content-Type: application/json-patch+json" -d "{\"userName\":\"string\",\"password\":\"string\"}"```





 