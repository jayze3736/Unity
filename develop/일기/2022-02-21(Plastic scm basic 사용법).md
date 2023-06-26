# 기본적인 unity에서의 plastic scm 사용법
팀 프로젝트의 경우 수행한 작업을 프로젝트로 한 군데 모으기 위한 시스템이 필요하다. 이전에는 unity collab을 이용하여 집과 센터에서도 분산 작업을 할 수 있었는데, 이번 유니티에서
정책이 바뀌었는지 plastic scm을 사용하게 되었다. 따라서 다음 링크의 영상을 보고 필요한 기본적인 사용법만을 정리하려고 한다.
   
   링크: https://www.youtube.com/watch?v=PjPK6hxGUFU


## 첫번째, 브랜치 생성

![제목 없음](https://user-images.githubusercontent.com/79313194/154908002-a5146d4d-f574-4af0-b432-766bee99b7c9.png)

브랜치를 생성하여 팀원들의 작업 진행과 기록을 구별을 쉽게할 수 있다.

## 워크 스페이스 전환

![제목 없음](https://user-images.githubusercontent.com/79313194/154908956-5cce8ddb-8970-41f0-b416-c80b14f94371.png)

동그라미 하나는 체인지 세트(changeset)이라고 한다. 

![image](https://user-images.githubusercontent.com/79313194/154909282-7aa158dc-221f-4a67-b474-91842d98eac2.png)

동그라미를 더블 클릭하여 확인해보면 어떤 변경이 이루어졌는지 확인할 수 있다.


![제목 없음](https://user-images.githubusercontent.com/79313194/154909497-a3062319-8fd0-44f7-8429-55b1b2d486c8.png)

***"이 체인지 세트로 워크 스페이스 전환"*** 을 누르면 체인지 세트가 발생했던 프로젝트로 돌아갈 수 있다. 다시 말해서 프로젝트 진행을 하다가 문제가
발생해서 다시 되돌아가야한다면 해당 기능을 이용하면 복구가 가능하다.

## 체인지 세트 병합
![제목 없음](https://user-images.githubusercontent.com/79313194/154910020-5ef924fa-fe43-47ef-af87-d8cc7454cbd2.png)

브랜치로 갈라 나누어 작업을 분산 시, 다시 하나로 합쳐야 한다. 따라서 현재 워크스페이스에 병합하길 원하는 체인지 세트에 왼쪽 클릭 후 "이 체인지 세트에서 병합"을 누르면
분산한 작업을 하나의 프로젝트로 병합하는 것이 가능하다.

## 체크인
![image](https://user-images.githubusercontent.com/79313194/154911001-bd149696-f724-4e1b-8c7f-b62b3444cc5c.png)

scene에 empty object를 추가하면 프로젝트 내에 변경사항이 생긴것이고 이 변경사항을 허용할 지 안 할지 묻는다. 여기서 체크인을 누르면 해당 변경을 허용하겠다는 의미가 되며 
change set이 생긴다. 

![제목 없음](https://user-images.githubusercontent.com/79313194/154911480-f1d096be-66ef-4f43-963d-94be105a2355.png)


