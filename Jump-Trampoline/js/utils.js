'use strict';

/* EXTENSIONES DE JQUERY */
(function($){
    // Revisa si el primer elemento de la lista contiene cierto evento o handler de jQuery.
    $.fn.hasEvent = function(type) {
        var data = jQuery._data(this[0], 'events')[type];
        if (data === undefined || data.length === 0){
            return false;
        }
        return true;
    };

    // Permite leer las variables de CSS del elemento seleccionado.
    $.fn.getCssProperty = function(propertyName){
        var styles = window.getComputedStyle(this[0]);
        return styles.getPropertyValue(propertyName).trim();
    };

    // Permite remover clases mediante wildcards (ej: col-*) 
    $.fn.alterClass = function ( removals, additions ) {
	    var self = this;
	    if ( removals.indexOf( '*' ) === -1 ) {
		    // Use native jQuery methods if there is no wildcard matching
		    self.removeClass( removals );
		    return !additions ? self : self.addClass( additions );
	    }
	    var patt = new RegExp( '\\s' + 
			    removals.
				    replace( /\*/g, '[A-Za-z0-9-_]+' ).
				    split( ' ' ).
				    join( '\\s|\\s' ) + 
			    '\\s', 'g' );
	    self.each( function ( i, it ) {
		    var cn = ' ' + it.className + ' ';
		    while ( patt.test( cn ) ) {
			    cn = cn.replace( patt, ' ' );
		    }
		    it.className = $.trim( cn );
	    });
	    return !additions ? self : self.addClass( additions );
    };
}(jQuery));


/* UTILIDADES GENERALES */
function clamp(value, min, max) {
    if (value < min) {
	    return min;
    }
    else if (value > max) {
	    return max;
    }
    return value;
}
function lerp(value1, value2, amount) {
	amount = amount < 0 ? 0 : amount;
	amount = amount > 1 ? 1 : amount;
	return value1 + (value2 - value1) * amount;
}

function hexToRGB(hex, alpha) {
    var r = parseInt(hex.slice(1, 3), 16),
        g = parseInt(hex.slice(3, 5), 16),
        b = parseInt(hex.slice(5, 7), 16);

    if (alpha) {
        return "rgba(" + r + ", " + g + ", " + b + ", " + alpha + ")";
    } else {
        return "rgb(" + r + ", " + g + ", " + b + ")";
    }
}


/* UTILIDADES PARA ASP. */
function toggleTheme(){
    var currentTheme = document.documentElement.getAttribute('data-theme');
    setTheme(currentTheme == 'light' ? 'dark' : 'light');
}
function setTheme(theme){
    if(theme == 'dark') document.documentElement.setAttribute('data-theme', 'dark');
    else document.documentElement.setAttribute('data-theme', 'light');
    Cookies.set('fincloud-theme', theme, { expires: 7 });
}
function setColorScheme(colorScheme){
    document.documentElement.setAttribute('data-color-scheme', colorScheme);
}
setTheme(Cookies.get('fincloud-theme'));


// Sobrescritura del Page_ClientValidate para evitar que bloquee el formulario cuando se hace de forma manual.
function DoPageClientValidate(validationGroupName) {
    var result = Page_ClientValidate(validationGroupName);
    Page_BlockSubmit = false;
    return result; 
}

function bloquearVolverAtras(){
    history.pushState(null, document.title, location.href);
    window.addEventListener('popstate', function(event) {
        history.pushState(null, document.title, location.href);
    });
}

function isNumber(evt) {
    var charCode=(evt.which)?evt.which:evt.keyCode;
    if(charCode>31&&(charCode<48||charCode>57)) return false;
    return true;
}

function isNumberNegative(evt) {
    var charCode=(evt.which)?evt.which:evt.keyCode;
    if(charCode>31 && charCode!=45 && (charCode<48 || charCode>57)) return false;
    return true;    
}

function addDots(temp) {
    if(temp.value == "") return;
    var numStr = temp.value.replace(".", "").replace(".", "").replace(".", "").replace(".", "").replace(".", "");
    var num = parseInt(numStr);
    temp.value = num.toLocaleString('es-CL');
}

/*function TryParseInt(str, defultValue){
    try{
        return parseInt(str);
    } catch(error){
        return defaultValue;
    }
}*/
function TryParseInt(str,defaultValue) {
    var retValue=defaultValue;
    if(str!==null) {
        if(str.length>0) {
            if(!isNaN(str)) {
                retValue=parseInt(str);
            }
        }
    }
    return retValue;
}

function obtenerRut(txt) {
    var newTxt=txt.split('(');
    var rut="";
    for(var i=1;i<newTxt.length;i++) {
        rut=newTxt[i].split(')')[0];
    }
    return rut;
}

function obtenerRazonSocial(txt) {
    return txt.substr(0,txt.lastIndexOf('(')).trim();
}

function autocomplete(inp,arr,idCvProveedor='',idTxtNombreProveedor='') {
    var currentFocus;

    inp.addEventListener("input",function(e) {
        var a,b,i,val=this.value;
        closeAllLists();
        if(!val) { return false; }
        currentFocus=-1;
        a=document.createElement("DIV");
        a.setAttribute("id",this.id+"autocomplete-list");
        a.setAttribute("class","autocomplete-items");
        this.parentNode.appendChild(a);
        for(i=0;i<arr.length;i++) {
            if(arr[i].toUpperCase().includes(val.toUpperCase())) {
                b=document.createElement("DIV");
                var res=arr[i].toUpperCase();
                b.innerHTML=res.replace(val.toUpperCase(),"<strong>"+val.toUpperCase()+"</strong>");
                b.innerHTML+="<input type='hidden' value='"+arr[i]+"'>";

                //Click de un div de autocompletar
                b.addEventListener("click",function(e) {
                    var seleccionado = this.getElementsByTagName("input")[0].value;

                    //Si tenemos un txtproveedor, ponemos el rut en este input, y el nombre en el otro txt
                    if(idTxtNombreProveedor != ''){
                        inp.value = obtenerRut(seleccionado);
                        $('#' + idTxtNombreProveedor).val(obtenerRazonSocial(seleccionado));

                    } else{
                        inp.value = seleccionado;
                    }
                    closeAllLists();
                });
                a.appendChild(b);
            }
        }
    });

    inp.addEventListener("keydown",function(e) {
        var x=document.getElementById(this.id+"autocomplete-list");
        if(x) x=x.getElementsByTagName("div");
        if(e.keyCode==40) {
            currentFocus++;
            addActive(x);
        } else if(e.keyCode==38) {
            currentFocus--;
            addActive(x);
        } else if(e.keyCode==13) {
            e.preventDefault();
            if(currentFocus>-1) {
                if(x) x[currentFocus].click();
            }
        }
    });

    function addActive(x) {
        if(!x) return false;
        removeActive(x);
        if(currentFocus>=x.length) currentFocus=0;
        if(currentFocus<0) currentFocus=(x.length-1);
        x[currentFocus].classList.add("autocomplete-active");
    }

    function removeActive(x) {
        for(var i=0;i<x.length;i++) {
            x[i].classList.remove("autocomplete-active");
        }
    }

    function closeAllLists(elmnt) {
        var x=document.getElementsByClassName("autocomplete-items");
        for(var i=0;i<x.length;i++) {
            if(elmnt!=x[i]&&elmnt!=inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }

        //Validamos manualmente el textbox cuando el autocompletar se cierra para no cometer errores
        if(idCvProveedor != ''){
            var cv=document.getElementById(idCvProveedor);
            ValidatorValidate(cv);
        }
    }

    document.addEventListener("click",function(e) {
        closeAllLists(e.target);
    });
}



/* MULTI INPUT RANGE */
function asignacionGastosDocumentReady(idDDLAsignacionGasto, idDDLCentroCosto, idHFAsignacionGasto){
    $('#'+idDDLAsignacionGasto).change(function(){
        var idAsignacionGasto = $(this).val();
        $('#'+idDDLCentroCosto).toggleClass('d-none', idAsignacionGasto != '0');
        $('[id^="panelEsq-"]').addClass('d-none');
        $('#panelEsq-'+idAsignacionGasto).removeClass('d-none');
        $('#panelEsq-'+idAsignacionGasto+' .btn-link').click();
    });

    $("[id^='inputRange-']").on("input change", function() {
        var idEntidad = $(this).attr('id').split('-')[1];
        repartir(idEntidad, $(this).val());
    });
    $("[id^='inputNumber-']").on("input change", function() {
        var idEntidad = $(this).attr('id').split('-')[1];
        repartir(idEntidad, $(this).val() == "" ? 0 : $(this).val());
    });

    //Solamente cuando soltamos el slider, definimos los valores finales de los elementos.
    $("[id^='inputRange-']").on("mouseup touchend", function(){ definirValoresFinales(idHFAsignacionGasto); });
    $("[id^='inputNumber-']").on("blur", function(){ definirValoresFinales(idHFAsignacionGasto); });
}
function definirValoresFinales(idHFAsignacionGasto){
    var asignacionGasto = "";
    $("[id^='inputRange-']").each(function(i){
        var idEntidad = $(this).attr('id').split('-')[1];
        var valor = $(this).val() == "" ? 0 : (Math.round($(this).val() * 10) / 10);

        //Solo usamos el valor redondeado para los valores finales.
        $(this).data('inicial', valor);
        if(valor > 0) asignacionGasto += (idEntidad + '-' + valor + ',');
    });
    $('#'+idHFAsignacionGasto).val(asignacionGasto);
}
function repartir(idEntidadInicial, valor){
    var valorInicial = $("#inputRange-"+idEntidadInicial).data('inicial');
    if(valor == valorInicial) return;
    var restanteInicial = 100 - valorInicial;
    var restante = 100 - valor;
    cambiarValor(idEntidadInicial, valor);

    var cuenta = 0.0;
    //Esto está bien, pues el que acabamos de modificar no debería ser modificado, sino los demás adaptarse.
    var inputs = $("[id^='inputRange-']:not(#inputRange-"+idEntidadInicial+")");
    inputs.each(function(i){
        var idEntidad = $(this).attr('id').split('-')[1];
        var nuevoValor;

        //Si somos los últimos de la lista, tomamos lo que nos quede.
        if(i == inputs.length - 1){
            nuevoValor = restante - cuenta;

        //Si no somos los últimos calculamos nuestro tamaño.
        } else{
            var valorEntidad = $(this).data('inicial');
            nuevoValor = restanteInicial == 0 ? (restante / inputs.length) : (restante * (valorEntidad / restanteInicial));
            cuenta += nuevoValor;
        }

        cambiarValor(idEntidad, nuevoValor);
    });
}
function repartirEquitativamente(idHFAsignacionGasto){
    var cuenta = 0;
    var inputs = $("[id^='inputRange-']");
    var valor = (Math.round((100 / inputs.length) * 10) / 10);

    inputs.each(function(i){
        var idEntidad = $(this).attr('id').split('-')[1];
        cambiarValor(idEntidad, (i == inputs.length - 1) ? (100 - cuenta) : valor);
        cuenta += valor;
    });

    definirValoresFinales(idHFAsignacionGasto);
}
function cambiarValor(idEntidad, valor){
    $("#inputNumber-"+idEntidad).val(Math.round(valor * 10) / 10);
    $("#inputRange-"+idEntidad).val(valor);
}




/* CUSTOM VALIDATORS */ 
function validarArchivoAdjunto(oSrc,args) {
    var rutaAdjunto=$('#' + oSrc.controltovalidate).val().split('//');
    var nombreAdjunto=rutaAdjunto[rutaAdjunto.length-1];

    var re=/(?:\.([^.]+))?$/;
    var extension=re.exec(nombreAdjunto)[1];
    if(extension!=undefined) {
        var ext=extension.toUpperCase();
        if(ext=='PDF'||ext=='PNG'||ext=='JPG'||ext=='JPEG'||ext=='GIF') {
            args.IsValid=true;
            return;
        }
    }
    args.IsValid=false;
}



    /* CONTROLES ASP */
    // Hace que un CheckBoxList se comporte como un RadioButtonList; poner en OnClientClick
    function MutuallyExclusiveCheckBoxList(checkBoxList, event) {
        var chks = checkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].type == 'checkbox') {
                chks[i].checked = false;
            }
        }
        event.target.checked = true;
    }

    function EnableCheckBoxList(checkBoxList, enabled) {
        //COMO HACER PARA HABILITARLOS DESDE EL SERVER D:
        $(checkBoxList).toggleClass('aspNetDisabled', !enabled);
        $(checkBoxList).find('span').toggleClass('aspNetDisabled', !enabled);
        $(checkBoxList).attr('disabled', !enabled);
        var chks = checkBoxList.getElementsByTagName("input");
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].type == 'checkbox') {
                chks[i].disabled = !enabled;
                chks[i].checked = false;
            }
        }
    }





/* EXTENSIONES DE BOOTSTRAP */
function abrirModalSinAnimacion(id) { 
    $(id).modal('show');
    $(id).removeClass('fade');
    setTimeout(function(){
        $(id).addClass('fade');
    }, 1000);
}





/* MODIFICACIONES DE CHART.JS */

//Tipo de gráfico roundedBar; utiliza la opción barRoundness para controlar el efecto.
// draws a rectangle with a rounded top
Chart.helpers.drawRoundedTopRectangle = function(ctx, x, y, width, height, radius) {
    ctx.beginPath();
    ctx.moveTo(x + radius, y);
    // top right corner
    ctx.lineTo(x + width - radius, y);
    ctx.quadraticCurveTo(x + width, y, x + width, y + radius);
    // bottom right   corner
    ctx.lineTo(x + width, y + height);
    // bottom left corner
    ctx.lineTo(x, y + height);
    // top left   
    ctx.lineTo(x, y + radius);
    ctx.quadraticCurveTo(x, y, x + radius, y);
    ctx.closePath();
};

Chart.elements.RoundedTopRectangle = Chart.elements.Rectangle.extend({
  draw: function() {
    var ctx = this._chart.ctx;
    var vm = this._view;
    var left, right, top, bottom, signX, signY, borderSkipped;
    var borderWidth = vm.borderWidth;

    if (!vm.horizontal) {
      // bar
      left = vm.x - vm.width / 2;
      right = vm.x + vm.width / 2;
      top = vm.y;
      bottom = vm.base;
      signX = 1;
      signY = bottom > top? 1: -1;
      borderSkipped = vm.borderSkipped || 'bottom';

    } else {
      // horizontal bar
      left = vm.base;
      right = vm.x;
      top = vm.y - vm.height / 2;
      bottom = vm.y + vm.height / 2;
      signX = right > left? 1: -1;
      signY = 1;
      borderSkipped = vm.borderSkipped || 'left';
    }

    // Canvas doesn't allow us to stroke inside the width so we can
    // adjust the sizes to fit if we're setting a stroke on the line
    if (borderWidth) {
      // borderWidth shold be less than bar width and bar height.
      var barSize = Math.min(Math.abs(left - right), Math.abs(top - bottom));
      borderWidth = borderWidth > barSize? barSize: borderWidth;
      var halfStroke = borderWidth / 2;
      // Adjust borderWidth when bar top position is near vm.base(zero).
      var borderLeft = left + (borderSkipped !== 'left'? halfStroke * signX: 0);
      var borderRight = right + (borderSkipped !== 'right'? -halfStroke * signX: 0);
      var borderTop = top + (borderSkipped !== 'top'? halfStroke * signY: 0);
      var borderBottom = bottom + (borderSkipped !== 'bottom'? -halfStroke * signY: 0);
      // not become a vertical line?
      if (borderLeft !== borderRight) {
        top = borderTop;
        bottom = borderBottom;
      }
      // not become a horizontal line?
      if (borderTop !== borderBottom) {
        left = borderLeft;
        right = borderRight;
      }
    }
    

    // Método para calcular si somos la última sección de esta barra.
    var lastOfStack = 0;
    var currentStack = this._chart.getDatasetMeta(this._datasetIndex).stack;
    for (var i = 0; i < this._chart.data.datasets.length; i++) {
        //No tomamos en cuenta este dataset si no pertene al mismo stack de nosotros
        if(this._chart.data.datasets[i].stack != currentStack) continue;
        if(this._chart.data.datasets[i].data[this._index] > 0) lastOfStack = i;
    }


    var rounded = vm.y < vm.base && lastOfStack == this._datasetIndex; // <-- Esto indica que tenemos datos en este bloque.

    
    // calculate the bar width and roundess
    var barWidth = Math.abs(left - right);
    var roundness = rounded ? (this._chart.config.options.barRoundness || 0.1) : 0;
    var radius = barWidth * roundness * 0.2;

    // keep track of the original top of the bar
    var prevTop = top;

    // move the top down so there is room to draw the rounded top
    top = prevTop + radius;
    var barRadius = top - prevTop;

    ctx.beginPath();
    ctx.fillStyle = vm.backgroundColor;
    ctx.strokeStyle = vm.borderColor;
    ctx.lineWidth = borderWidth;

    // draw the rounded top rectangle
    Chart.helpers.drawRoundedTopRectangle(ctx, left, (top - barRadius + 1), barWidth, bottom - prevTop, barRadius);

    ctx.fill();
    if (borderWidth) {
      ctx.stroke();
    }

    // restore the original top value so tooltips and scales still work
    top = prevTop;
  },
});
  
Chart.defaults.roundedBar = Chart.helpers.clone(Chart.defaults.bar);
Chart.controllers.roundedBar = Chart.controllers.bar.extend({
    dataElementType: Chart.elements.RoundedTopRectangle
});

//Modificacion Angelica
Chart.controllers.HorizontalBar = Chart.controllers.bar.extend({
    updateElement: function updateElement(rectangle, index, reset, numBars) {

        var xScale = this.getScaleForId(this.getDataset().xAxisID);
        var yScale = this.getScaleForId(this.getDataset().yAxisID);

        var xScalePoint;

        if (xScale.min < 0 && xScale.max < 0) {
            xScalePoint = xScale.getPixelForValue(xScale.max);
        } else if (xScale.min > 0 && xScale.max > 0) {
            xScalePoint = xScale.getPixelForValue(xScale.min);
        } else {
            xScalePoint = xScale.getPixelForValue(0);
        }

        Chart.helpers.extend(rectangle, {
            _chart: this.chart.chart,
            _xScale: xScale,
            _yScale: yScale,
            _datasetIndex: this.index,
            _index: index,

            _model: {
                x: reset ? xScalePoint : this.calculateBarX(index, this.index),
                y: this.calculateBarY(index, this.index),
                label: this.chart.data.labels[index],
                datasetLabel: this.getDataset().label,
                base: this.calculateBarBase(this.index, index),
                height: this.calculateBarHeight(numBars),
                backgroundColor: rectangle.custom && rectangle.custom.backgroundColor ? rectangle.custom.backgroundColor : Chart.helpers.getValueAtIndexOrDefault(this.getDataset().backgroundColor, index, this.chart.options.elements.rectangle.backgroundColor),
                borderColor: rectangle.custom && rectangle.custom.borderColor ? rectangle.custom.borderColor : Chart.helpers.getValueAtIndexOrDefault(this.getDataset().borderColor, index, this.chart.options.elements.rectangle.borderColor),
                borderWidth: rectangle.custom && rectangle.custom.borderWidth ? rectangle.custom.borderWidth : Chart.helpers.getValueAtIndexOrDefault(this.getDataset().borderWidth, index, this.chart.options.elements.rectangle.borderWidth),
            },

            // override the draw and inRange functions because the one in the library needs width (we only have height)

            draw: function () {
                var ctx = this._chart.ctx;
                ctx.fillStyle = this._view.backgroundColor;
                ctx.fillRect(this._view.base, this._view.y - this._view.height / 2, this._view.x - this._view.base, this._view.height);

                ctx.strokeStyle = this._view.borderColor;
                ctx.strokeWidth = this._view.borderWidth;
                ctx.strokeRect(this._view.base, this._view.y - this._view.height / 2, this._view.x - this._view.base, this._view.height);
            },

            inRange: function (mouseX, mouseY) {
                var vm = this._view;
                var inRange = false;

                if (vm) {
                    if (vm.x < vm.base) {
                        inRange = (mouseY >= vm.y - vm.height / 2 && mouseY <= vm.y + vm.height / 2) && (mouseX >= vm.x && mouseX <= vm.base);
                    } else {
                        inRange = (mouseY >= vm.y - vm.height / 2 && mouseY <= vm.y + vm.height / 2) && (mouseX >= vm.base && mouseX <= vm.x);
                    }
                }

                return inRange;
            }
        });

        rectangle.pivot();

        // the animation progresses _view values from their current value to the _model value
        rectangle._view.x = rectangle._model.base;
    },

    calculateBarBase: function (datasetIndex, index) {
        var xScale = this.getScaleForId(this.getDataset().xAxisID);
        var yScale = this.getScaleForId(this.getDataset().yAxisID);

        var base = 0;

        if (xScale.options.stacked) {
            var value = this.chart.data.datasets[datasetIndex].data[index];

            if (value < 0) {
                for (var i = 0; i < datasetIndex; i++) {
                    var negDS = this.chart.data.datasets[i];
                    if (Chart.helpers.isDatasetVisible(negDS) && negDS.xAxisID === xScale.id) {
                        base += negDS.data[index] < 0 ? negDS.data[index] : 0;
                    }
                }
            } else {
                for (var j = 0; j < datasetIndex; j++) {
                    var posDS = this.chart.data.datasets[j];
                    if (Chart.helpers.isDatasetVisible(posDS) && posDS.xAxisID === xScale.id) {
                        base += posDS.data[index] > 0 ? posDS.data[index] : 0;
                    }
                }
            }

            return xScale.getPixelForValue(base);
        }

        base = xScale.getPixelForValue(xScale.min);

        if (xScale.beginAtZero || ((xScale.min <= 0 && xScale.max >= 0) || (xScale.min >= 0 && xScale.max <= 0))) {
            base = xScale.getPixelForValue(0, 0);
        } else if (xScale.min < 0 && xScale.max < 0) {
            base = xScale.getPixelForValue(xScale.max);
        }

        return base;
    },

    getRuler: function () {
        var xScale = this.getScaleForId(this.getDataset().xAxisID);
        var yScale = this.getScaleForId(this.getDataset().yAxisID);
        var datasetCount = this.getBarCount();

        var tickHeight = (function () {
            var min = yScale.getPixelForTick(1) - yScale.getPixelForTick(0);
            for (var i = 2; i < this.getDataset().data.length; i++) {
                min = Math.min(yScale.getPixelForTick(i) - yScale.getPixelForTick(i - 1), min);
            }
            return min;
        }).call(this);
        var categoryHeight = tickHeight * yScale.options.categoryPercentage;
        var categorySpacing = (tickHeight - (tickHeight * yScale.options.categoryPercentage)) / 2;
        var fullBarHeight = categoryHeight / datasetCount;
        var barHeight = fullBarHeight * yScale.options.barPercentage;
        var barSpacing = fullBarHeight - (fullBarHeight * yScale.options.barPercentage);

        return {
            datasetCount: datasetCount,
            tickHeight: tickHeight,
            categoryHeight: categoryHeight,
            categorySpacing: categorySpacing,
            fullBarHeight: fullBarHeight,
            barHeight: barHeight,
            barSpacing: barSpacing,
        };
    },

    calculateBarHeight: function () {
        var yScale = this.getScaleForId(this.getDataset().yAxisID);
        var ruler = this.getRuler();

        if (yScale.options.stacked) {
            return ruler.categoryHeight;
        }

        return ruler.barHeight;
    },

    calculateBarY: function (index, datasetIndex) {
        var yScale = this.getScaleForId(this.getDataset().yAxisID);
        var xScale = this.getScaleForId(this.getDataset().xAxisID);
        var barIndex = this.getBarIndex(datasetIndex);

        var ruler = this.getRuler();
        var leftTick = yScale.getPixelForValue(null, index, datasetIndex, this.chart.isCombo);
        leftTick -= this.chart.isCombo ? (ruler.tickHeight / 2) : 0;

        if (yScale.options.stacked) {
            return leftTick + (ruler.categoryHeight / 2) + ruler.categorySpacing;
        }

        return leftTick +
            (ruler.barHeight / 2) +
            ruler.categorySpacing +
            (ruler.barHeight * barIndex) +
            (ruler.barSpacing / 2) +
            (ruler.barSpacing * barIndex);
    },

    calculateBarX: function (index, datasetIndex) {
        var xScale = this.getScaleForId(this.getDataset().xAxisID);
        var yScale = this.getScaleForId(this.getDataset().yAxisID);

        var value = this.getDataset().data[index];

        if (xScale.options.stacked) {
            var sumPos = 0,
                sumNeg = 0;

            for (var i = 0; i < datasetIndex; i++) {
                var ds = this.chart.data.datasets[i];
                if (Chart.helpers.isDatasetVisible(ds)) {
                    if (ds.data[index] < 0) {
                        sumNeg += ds.data[index] || 0;
                    } else {
                        sumPos += ds.data[index] || 0;
                    }
                }
            }

            if (value < 0) {
                return xScale.getPixelForValue(sumNeg + value);
            } else {
                return xScale.getPixelForValue(sumPos + value);
            }

            return xScale.getPixelForValue(value);
        }

        return xScale.getPixelForValue(value);
    }
});

// Fuente: https://stackoverflow.com/questions/36934967/chart-js-doughnut-with-rounded-edges
Chart.defaults.RoundedDoughnut    = Chart.helpers.clone(Chart.defaults.doughnut);
Chart.controllers.RoundedDoughnut = Chart.controllers.doughnut.extend({
    draw: function(ease) {
        var ctx           = this.chart.ctx;
        var easingDecimal = ease || 1;
        var arcs          = this.getMeta().data;
        Chart.helpers.each(arcs, function(arc, i) {
            arc.transition(easingDecimal).draw();

            var pArc   = arcs[i === 0 ? arcs.length - 1 : i - 1];
            var pColor = pArc._view.backgroundColor;

            var vm         = arc._view;
            var radius     = (vm.outerRadius + vm.innerRadius) / 2;
            var thickness  = (vm.outerRadius - vm.innerRadius) / 2;
            var startAngle = Math.PI - vm.startAngle - Math.PI / 2;
            var angle      = Math.PI - vm.endAngle - Math.PI / 2;

            ctx.save();
            ctx.translate(vm.x, vm.y);

            ctx.fillStyle = i === 0 ? vm.backgroundColor : pColor;
            ctx.beginPath();
            ctx.arc(radius * Math.sin(startAngle), radius * Math.cos(startAngle), thickness, 0, 2 * Math.PI);
            ctx.fill();

            ctx.fillStyle = vm.backgroundColor;
            ctx.beginPath();
            ctx.arc(radius * Math.sin(angle), radius * Math.cos(angle), thickness, 0, 2 * Math.PI);
            ctx.fill();

            ctx.restore();
        });
    }
});

//Fuente: https://stackoverflow.com/questions/31399991/how-can-i-sort-insert-in-between-and-update-full-dataset-in-chartjs
var BarChartMethods = {
    // sort a dataset
    sort: function (chart, datasetIndex) {
        var data = []
        chart.datasets.forEach(function (dataset, i) {
            dataset.bars.forEach(function (bar, j) {
                if (i === 0) {
                    data.push({
                        label: chart.scale.xLabels[j],
                        values: [bar.value]
                    })
                } else 
                    data[j].values.push(bar.value)
                    });
        })
        
        data.sort(function (a, b) {
            if (a.values[datasetIndex] > b.values[datasetIndex])
                return -1;
            else if (a.values[datasetIndex] < b.values[datasetIndex])
                return 1;
            else
                return 0;
        })
        
        chart.datasets.forEach(function (dataset, i) {
            dataset.bars.forEach(function (bar, j) {
                if (i === 0)
                    chart.scale.xLabels[j] = data[j].label;
                bar.label = data[j].label;
                bar.value = data[j].values[i];
            })
        });
        chart.update();
    },
    // reload data
    reload: function (chart, datasetIndex, labels, values) {
        var diff = chart.datasets[datasetIndex].bars.length - values.length;
        if (diff < 0) {
            for (var i = 0; i < -diff; i++)
                chart.addData([0], "");
        } else if (diff > 0) {
            for (var i = 0; i < diff; i++)
                chart.removeData();
        }
        
        chart.datasets[datasetIndex].bars.forEach(function (bar, i) {
            chart.scale.xLabels[i] = labels[i];
            bar.value = values[i];
        })
        chart.update();
    }
}
