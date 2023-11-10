(function () {
    var container = document.querySelector('.bodyImg2');
    var images = document.querySelectorAll('.bodyImg2 img');
    var imageCount = images.length;
    var scrollBarWidth = 10; // 根据需要调整滚动条的宽度，这里假设为10px  

    // 创建水平滚动框  
    var scrollContainer = document.createElement('div');
    scrollContainer.style.width = '100%';
    scrollContainer.style.overflow = 'auto';
    scrollContainer.style.whiteSpace = 'nowrap';
    container.appendChild(scrollContainer);

    // 将图片添加到水平滚动框中  
    images.forEach(function (image) {
        scrollContainer.appendChild(image);
    });

    // 创建水平滚动条  
    var scrollBar = document.createElement('div');
    scrollBar.style.height = '100%';
    scrollBar.style.width = scrollBarWidth + 'px';
    scrollBar.style.backgroundColor = '#ccc'; // 根据需要调整滚动条的颜色  
    scrollBar.style.cursor = 'ew-resize'; // 设置鼠标拖动滚动条时的图标样式  
    scrollBar.style.position = 'absolute';
    scrollBar.style.top = '0';
    scrollBar.style.right = '0';
    container.appendChild(scrollBar);

    var startX = 0;
    var endX = 0;
    var isDragging = false;

    // 监听鼠标按下事件，开始拖动滚动条  
    scrollBar.addEventListener('mousedown', function (event) {
        isDragging = true;
        startX = event.clientX;
    });

    // 监听鼠标移动事件，拖动滚动条  
    document.addEventListener('mousemove', function (event) {
        if (isDragging) {
            endX = event.clientX;
            var deltaX = endX - startX; // 计算鼠标移动的距离  
            if (Math.abs(deltaX) > 5) { // 根据需要调整移动的灵敏度，这里假设移动距离超过5px时才进行移动操作  
                startX = endX; // 更新起始点位置  
                var scrollLeft = scrollContainer.scrollLeft; // 获取当前滚动的位置  
                scrollLeft += deltaX > 0 ? 1 : -1; // 根据鼠标移动方向调整滚动的位置，每次移动1个图片的位置  
                scrollContainer.scrollLeft = scrollLeft; // 执行滚动操作  
            }
        } else { // 如果不是拖动状态，则只响应鼠标滚轮滚动事件  
            var scrollLeft = scrollContainer.scrollLeft; // 获取当前滚动的位置  
            var scrollStep = 10; // 根据需要调整滚动的步长，这里假设每次滚动10px  
            if (event.deltaY < 0) { // 如果鼠标滚轮向上滚动，则向前滚动图片  
                if (scrollLeft < images[imageCount - 1].offsetLeft) { // 如果已经滚动到最后一张图片，则不进行滚动操作  
                    return;
                }
                scrollLeft += scrollStep; // 更新滚动的位置  
                scrollContainer.scrollLeft = scrollLeft; // 执行滚动操作  
            } else { // 如果鼠标滚轮向下滚动，则向后滚动图片
                if (scrollLeft > images[0].offsetLeft) { // 如果已经滚动到第一张图片，则不进行滚动操作  
          return;  
        }  
        scrollLeft -= scrollStep; // 更新滚动的位置  
        document.querySelector('.bodyImg2').scrollLeft = scrollLeft; // 执行滚动操作  
      }  
    }  
  });  
  
  document.addEventListener('mouseup', function() {  
    isDragging = false; // 停止拖动操作，不再响应鼠标移动事件了  
  });  
})();