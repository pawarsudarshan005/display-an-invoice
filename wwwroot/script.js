// BUG FIXES:
//   'DOMContentLoade'  -> 'DOMContentLoaded'
//   'fetc'             -> 'fetch'
//   'resp.jsoon()'     -> 'resp.json()'
//   'item.prce'        -> 'item.price'
//   'console.eror'     -> 'console.error'
document.addEventListener('DOMContentLoaded', function () {
    fetch('/api/invoice')
        .then(resp => {
            if (!resp.ok) {
                throw new Error('HTTP ' + resp.status);
            }
            return resp.json();
        })
        .then(data => {
            const container = document.getElementById('invoice-container');
            let total = 0;
            let html = '<ul>';
            data.items.forEach(item => {
                total += item.price;
                html += `<li>${item.name} - $${item.price.toFixed(2)}</li>`;
            });
            html += '</ul>';
            html += `<p class="total"><strong>Total: $${total.toFixed(2)}</strong></p>`;
            container.innerHTML = html;
        })
        .catch(err => {
            document.getElementById('invoice-container').innerHTML =
                '<p class="error">Failed to load invoice.</p>';
            console.error("Failed to load invoice:", err);
        });
});
