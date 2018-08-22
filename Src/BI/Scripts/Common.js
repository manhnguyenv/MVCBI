function createNamespace(namespaceString) {
    var parts = namespaceString.split('.'),
        parent = window,
        currentPart = '';

    for (var i = 0, length = parts.length; i < length; i++) {
        currentPart = parts[i];
        parent[currentPart] = parent[currentPart] || {};
        parent = parent[currentPart];
    }

    return parent;
}

createNamespace("mvcBI");

String.prototype.replaceAll = function (search, replacement) {
    var target = this;
    return target.replace(new RegExp(search, 'g'), replacement);
};

window.addEventListener('load', function () {
    var a = window.location.pathname.split('/'); var i = a.length; var b = a[i - 1]; while (b === "") { i = i - 1; b = a[i]; }
    var i = 0;
    $('#side-menu li').each(function () {
        var self = this;
        $(self).removeClass("active");
        $(self).find('a').each(function () {
            var c = $(this).attr("href").replace(/ /g, '').replace(/\s/g, '').replace('/', '').replace('Admin/', '');
            if (c === b) {
                $(self).removeClass("active").addClass("active");
                var alink = $(self).find('a')[0];
                $(alink).removeClass("link-active").addClass("link-active");
                i = i + 1;
            }
        });
    });
    if (i === 0) {
        var member = $('#side-menu li a[href="/Admin/Dashboard"]');
        if (member.length !== 0) {
            var parent = $(member).parent();
            if (parent.length !== 0) {
                $(parent).removeClass("active").addClass("active");
            }
        }
    }
}, false);

mvcBI.padValue = function (value) {
  return (value < 10) ? "0" + value : value;
};

mvcBI.set_matching_words = function (selectObj, txtObj) {
  var letter = txtObj.value;
  for (var i = 0; i < selectObj.length; i++) {
    if (selectObj.options[i].value.charAt(0) == letter) {
      selectObj.options[i].selected = true;
    }
    else {
      selectObj.options[i].selected = false;
    }
  }
};

mvcBI.JSON_to_URLEncoded = function (element, key, list) {
  list = list || [];
  if (typeof (element) === 'object') {
    for (var idx in element)
      JSON_to_URLEncoded(element[idx], key ? key + '[' + idx + ']' : idx, list);
  } else {
    list.push(key + '=' + encodeURIComponent(element));
  }
  return list.join('&');
};

mvcBI.handleException = function (request, message, error) {
  var msg = "";
  msg += "Code: " + request.status + "\n";
  msg += "Text: " + request.statusText + "\n";
  if (request.responseJSON != null) {
    msg += "Message" +
      request.responseJSON.Message + "\n";
  }
  alert(msg);
};

mvcBI.exists = function () {
  return this.length !== 0;
};
