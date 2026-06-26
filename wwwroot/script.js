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
            const money = n => '$' + Number(n).toFixed(2);

            const rows = data.items.map(item => `
                <tr>
                    <td>${item.name}</td>
                    <td class="num">${item.quantity}</td>
                    <td class="num">${money(item.price)}</td>
                    <td class="num">${money(item.lineTotal)}</td>
                </tr>`).join('');

            container.innerHTML = `
                <header class="invoice-header">
                    <div class="brand">
                        <h1>INVOICE</h1>
                        <p class="company">Acme Corporation</p>
                        <p class="company-info">100 Market Street, San Francisco, CA 94103</p>
                        <p class="company-info">billing@acme.com &middot; +1 (555) 987-6543</p>
                    </div>
                    <div class="meta">
                        <p><span>Invoice No</span><strong>${data.invoiceNumber}</strong></p>
                        <p><span>Invoice Date</span><strong>${data.invoiceDate}</strong></p>
                        <p><span>Due Date</span><strong>${data.dueDate}</strong></p>
                    </div>
                </header>

                <section class="bill-to">
                    <h2>Bill To</h2>
                    <p class="cust-name">${data.customer.name}</p>
                    <p>${data.customer.address}</p>
                    <p>${data.customer.email} &middot; ${data.customer.phone}</p>
                </section>

                <table class="items">
                    <thead>
                        <tr>
                            <th>Item</th>
                            <th class="num">Qty</th>
                            <th class="num">Unit Price</th>
                            <th class="num">Amount</th>
                        </tr>
                    </thead>
                    <tbody>${rows}</tbody>
                </table>

                <div class="summary">
                    <p><span>Subtotal</span><span>${money(data.subtotal)}</span></p>
                    <p><span>Tax (${(data.taxRate * 100).toFixed(0)}%)</span><span>${money(data.tax)}</span></p>
                    <p class="grand-total"><span>Total</span><span>${money(data.total)}</span></p>
                </div>

                <footer class="invoice-footer">
                    <p>Thank you for your business!</p>
                </footer>`;
        })
        .catch(err => {
            document.getElementById('invoice-container').innerHTML =
                '<p class="error">Failed to load invoice.</p>';
            console.error("Failed to load invoice:", err);
        });
});
