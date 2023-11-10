var index = 0;
//效果
function ChangeImg() {
    index++;
    var a = document.getElementsByClassName("img-slide");
    if (index >= a.length) index = 0;
    for (var i = 0; i < a.length; i++) {
        a[i].style.display = 'none';
    }
    a[index].style.display = 'block';
}
//设置定时器，每隔两秒切换一次图片并开始动画效果
setInterval(function () {
    ChangeImg();
    // 添加以下代码以启动过渡效果
    setTimeout(function () {
        document.querySelector('.img-slide').style.opacity = 1;
    }, 200);  // 这个时间应根据实际情况进行调整，这里设置为10毫秒，以保证在切换图片后立即开始动画效果，但不会立即达到最大透明度，从而实现平滑过渡的效果。
}, 3000);  // 每隔3秒切换一次图片，同时启动动画效果。这个时间可以根据实际情况进行调整。