### 무엇인가 ?
사용자가 그린 터치 제스처가 어떤 모양인이 유추해주는 컴포넌트입니다.

<br/>
<br/>

### 왜 만들었는가 ?

과거 터치 제스처가 필요한 게임에 넣기 위해 만들었었다. 그 당시에 관련 자료들이 적어 해외 블로그들을 둘러보며 몇몇 아이디어를 참고해서 만들었던 기억이 난다. 또한  현재도 역시 관련 자료들이 많이 없기도 하고 추후 유니티 외에도 다른 곳에서도 응용 할 일이 생기지 않을까? 싶어서 정리하려한다.
<br/>

<img src = "https://velog.velcdn.com/images/tlsakch510/post/28d54ec5-7449-41cb-ad9b-c603adcce78e/image.gif">
<br/>
<br/>

### 어떤 원리인가 ?
결론부터 말하자면, 스와이프 방향들과 변곡점의 갯수로 유추하는것. 

먼저 스와이프 방향을 총 8방향으로 구분했다. 
<img src = "https://velog.velcdn.com/images/tlsakch510/post/a52802c0-cb74-47af-aec9-8f06f35144bc/image.png" width="300" height="300">

이후 구분된 방향을 토대로 변곡점, 터치 방향들을 기록한다.
변곡점, 터치방향을 기록하는 방법은 아래와 같다.


<img src = "https://velog.velcdn.com/images/tlsakch510/post/bfc6cec3-de73-4973-af5c-b8d57472e353/image.png" >


1. 터치를 시작한 지점의 좌표를 변수 p1에 저장한다.
2. 특정 주기마다 p1 기준으로 사용자가 터치 중인 좌표 방향을 기록한다
3. 다시 현재 터치중인 좌표를 p1에 저장하고 p1 좌표기준 현재 사용자가 터치하고 있는 방향이 2단계에서 기록한 방향과 같다면 2단계로 이동, 다르다면 4단계로 이동, 터치가 끝났다면 5단계로 이동

4. 방향이 변경되었다면 변곡점이 한개 생긴것, 이후 p1에 현재 좌표를 저장하고 다시 2단계로 이동
5. 터치가 끝났다면, 변곡점의 갯수와 기록된 터치방향 리스트로 터치 제스처를 추측한다
   ex) 변곡점 2개, left down -> right -> left up = 삼각형 추측
   ex) 변곡점 0개, up -> 직선 추측
