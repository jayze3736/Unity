## Scrollview - mask
ScrollView에서 viewport의 범위를 벗어날 정도의 수만큼 콘텐츠가 존재할때 mask를 체크하면 overflow되는 content들을 viewport의 사이즈에 한해서 보여준다.


(근데 기본적으로 scroll view 크기가 viewport사이즈랑 크거나 같으면 알아서 content들을 잘라서 보여준다.)



![제목 없음](https://user-images.githubusercontent.com/79313194/151146017-bd25dd9e-52b2-43de-b10c-5e8cec132b4a.png)

## Scrollview - content
content는 실제 보여주는 콘텐츠들의 부모 오브젝트인데, 이때 콘텐츠들은 UI element여야한다.   
즉, 일반 sprite는 보여줄 수 없고 UI image, UI button등 UI element들만 보여질 수 있으므로 주의해야한다.   
UI element로 구성되어있는 prefab 또한 content로 정할 수 있다.

![제목 없음](https://user-images.githubusercontent.com/79313194/151147065-04809c94-97ac-4f3c-9b22-38be36e0ad65.png)


## 고생했던 부분
1. contents 중에서 savefile이라는 prefab이 있는데, 이 프리팹은 tmp(text mesh pro), button을 포함한다.   
위로 드래그를 하면 그 prefab은 다른 button contents와는 다르게 밖으로 삐져나오는 경우가 있었는데 처음에는 tmp가 일반 UI 요소가 아니라서 그런줄 알았다.   
알고보니 tmp와 button을 그리기 위한 canvas가 savefile에 컴포넌트로 부착되어있었고 contents의 canvas와 충돌이 발생해서 이러한 원치않는 결과를 얻게 되었다.
